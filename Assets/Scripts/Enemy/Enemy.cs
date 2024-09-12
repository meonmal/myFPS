using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    /// <summary>
    /// ���� ���� �� �ִ� ����
    /// </summary>
    enum EnemyState
    {
        Idle,
        Move,
        Attack,
        Return,
        Damage,
        Die,
    }

    /// <summary>
    /// ���� ���� ����
    /// </summary>
    EnemyState m_State;

    /// <summary>
    /// �÷��̾� �߰� ����
    /// </summary>
    public float findDistance = 8.0f;

    /// <summary>
    /// �÷��̾� Ʈ������
    /// </summary>
    Transform player;

    /// <summary>
    /// ���� ���� ����
    /// </summary>
    public float attackDistance = 2.0f;

    /// <summary>
    /// �� �̵��ӵ�
    /// </summary>
    public float moveSpeed = 5.0f;

    /// <summary>
    /// ĳ���� ��Ʈ�ѷ� ������Ʈ
    /// </summary>
    CharacterController cc;

    /// <summary>
    /// ���� �ð�
    /// </summary>
    public float currentTime = 0.0f;

    /// <summary>
    /// ���� ������ �ð�
    /// </summary>
    public float attackDelay = 2.0f;

    /// <summary>
    /// ���ʹ��� ���ݷ�
    /// </summary>
    public int attackPower = 3;

    /// <summary>
    /// �ʱ� ��ġ �����
    /// </summary>
    Vector3 originposition;

    /// <summary>
    /// �̵� ���� ����
    /// </summary>
    public float moveDistance = 20.0f;


    /// <summary>
    /// ���� ü��
    /// </summary>
    public float hp = 15.0f;

    /// <summary>
    /// ���� �ִ� hp
    /// </summary>
    public float maxhp = 15.0f;

    /// <summary>
    /// ���� �߻� ���� ���ݷ�
    /// </summary>
    public int weponPower = 5;


    private void Start()
    {
        // ���� ó���� �ϴ� �ൿ�� ����.
        m_State = EnemyState.Idle;

        // �÷��̾��� Ʈ������ ������Ʈ ã��
        player = GameObject.Find("Player").transform;

        // ĳ���� ������Ʈ �޾ƿ���
        cc = GetComponent<CharacterController>();

        // ���� �ʱ� ��ġ ����
        originposition = transform.position;
    }

    private void Update()
    {
        // ���� ���¸� üũ�Ͽ� �ش� ���º��� Ư�� ����� �����ϰ� �����.
        switch (m_State)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Move:
                Move();
                break;
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Return:
                Return();
                break;
            case EnemyState.Damage:
                //Demage();
                break;
            case EnemyState.Die:
                //Die();
                break;
        }
    }

    public void Idle()
    {
        // �÷��̾ �߰߹��� �ȿ� ������ ����
        if(Vector3.Distance(transform.position, player.position) < findDistance)
        {
            m_State = EnemyState.Move;
            print("���� ��ȯ Idle -> Move");
        }
    }
    public void Move()
    {
        // ���� ��ġ�� �ʱ� ��ġ���� �̵� ���� ������ �Ѿ�ٸ� ����
        if (Vector3.Distance(transform.position, originposition) > moveDistance)
        {
            // ���� ���¸� Return���� ����
            m_State = EnemyState.Return;
            print("���� ��ȯ : Move -> Return");
        }
        // �÷��̾ ���� ���� �ۿ� �ִٸ� ����
        else if(Vector3.Distance(transform.position, player.position) > attackDistance)
        {   
            Vector3 dir = (player.position - transform.position).normalized;

            cc.Move(dir * moveSpeed * Time.deltaTime);
        }
        // ���� ��찡 �� �� �ƴ϶�� ����
        else
        {
            // ���� ���¸� Attack���� ��ȯ
            m_State = EnemyState.Attack;
            print("���� ��ȯ : Move -> Attack");
        }
    }

    public void Attack()
    {
        // �÷��̾ ���� ���� ���̶�� �÷��̾� ����
        if(Vector3.Distance(transform.position, player.position) < attackDistance)
        {
            // ���� �ð����� �÷��̾� ����
            currentTime += Time.deltaTime;

            if(currentTime > attackDelay)
            {
                player.GetComponent<PlayerMove>().DamageAction(attackPower);
                print("����");
                currentTime = 0;

            }
        }
        // �׷��� �ʴٸ� ����
        else
        {
            // ���� ���� ����(���߰� �Ǳ�)
            m_State = EnemyState.Move;
            print("������ȯ Attack -> Move");
            currentTime = 0;
        }
    }

    public void Return()
    {
        // �ʱ� ��ġ������ �Ÿ��� 0.1f �̻��̸� ����
        if(Vector3.Distance(transform.position , originposition) > 0.1f)
        {
            // �ʱ� ��ġ ������ �̵�
            Vector3 dir = (originposition - transform.position).normalized;
            cc.Move(dir * moveSpeed * Time.deltaTime);
        }
        // �׷��� �ʴٸ� ����
        else
        {
            // ���� ��ġ �ʱ�ȭ
            transform.position = originposition;
            // hp�� �ٽ� ȸ���Ѵ�.
            hp = maxhp;
            // ���� ���¸� ��ȯ�Ѵ�.
            m_State = EnemyState.Idle;
            print("���� ��ȯ Return -> Idle");
        }
    }

    public void Damage()
    {
        // �ǰ� ���¸� ó���ϱ� ���� �ڷ�ƾ
        StartCoroutine(DamageProcess());
    }

    /// <summary>
    /// ������ ó���� �ڷ�ƾ �Լ�
    /// </summary>
    /// <returns></returns>
    IEnumerator DamageProcess()
    {
        // �ǰ� ��� �ð���ŭ ��ٸ���.
        yield return new WaitForSeconds(0.5f);

        // ���� ���¸� �̵� ���·� ��ȯ
        m_State = EnemyState.Move;
        print("���� ��ȯ Demaged -> Move");
    }

    public void Die()
    {
        StopAllCoroutines();

        StartCoroutine(DieProcess());
    }

    private IEnumerator DieProcess()
    {
        // ĳ���� ������Ʈ�� ��Ȱ��ȭ ��Ű��
        cc.enabled = false;

        // 2���� �Ҹ�
        yield return new WaitForSeconds(2f);
        print("�Ҹ�");
        Destroy(gameObject);
    }

    /// <summary>
    /// ������ ���� �Լ�
    /// </summary>
    /// <param name="hitPower"></param>
    public void HitEnemy(int hitPower)
    {
        // ���� �ǰ����̰ų� ���� �����̰ų� ���ư��� ������ ��� �������� ���� ����.
        if (m_State == EnemyState.Damage || m_State == EnemyState.Die || m_State == EnemyState.Return)
        {
            return;
        }

        // �÷��̾��� ���ݷ¸�ŭ ü�� ���
        hp -= hitPower;

        // ü���� 0 �̻��̶�� ����
        if(hp > 0)
        {
            // ���¸� Attack���� ����
            m_State = EnemyState.Attack;
            print("���� ��ȯ Any state -> Damaged");
            Damage();
        }
        // �׷��� �ʴٸ� �������·� ��ȯ
        else
        {
            m_State = EnemyState.Die;
            print("���� ��ȯ any state -> Die");
            Die();
        }
    }

    
}

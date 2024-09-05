using System;
using System.Collections;
using System.Collections.Generic;
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

    private void Start()
    {
        // ���� ó���� �ϴ� �ൿ�� ����.
        m_State = EnemyState.Idle;

        // �÷��̾��� Ʈ������ ������Ʈ ã��
        player = GameObject.Find("Player").transform;

        // ĳ���� ������Ʈ �޾ƿ���
        cc = GetComponent<CharacterController>();
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
                Demage();
                break;
            case EnemyState.Die:
                Die();
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
        // �÷��̾ ���� ���� �ۿ� �ִٸ� ����
        if(Vector3.Distance(transform.position, player.position) > attackDistance)
        {
            // �̵� ���� ����
            Vector3 dir = (player.position - transform.position).normalized;

            // ĳ���� ��Ʈ�ѷ��� �̿��� �̵��ϱ�
            cc.Move(dir * moveSpeed * Time.deltaTime);
        }
        // if�� ��찡 �ƴϸ� ����
        else
        {
            m_State = EnemyState.Attack;
            print("���� ��ȯ Move -> Attack");
        }
    }

    public void Attack()
    {
        if(Vector3.Distance(transform.position, player.position) < attackDistance)
        {
            currentTime += Time.deltaTime;
            if(currentTime > attackDelay)
            {
                print("����");
                currentTime = 0;
            }
        }
    }

    public void Return()
    {

    }

    public void Demage()
    {

    }

    public void Die()
    {

    }


}

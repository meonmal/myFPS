using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    /// <summary>
    /// �̵� �ӵ�
    /// </summary>
    public float MoveSpeed = 7.0f;

    /// <summary>
    /// �÷��̾� ��Ʈ�ѷ�
    /// </summary>
    public CharacterController cc;

    /// <summary>
    /// �÷��̾� �߷�
    /// </summary>
    public float gravity = -20.0f;

    /// <summary>
    /// �÷��̾� ���� �ӷ�
    /// </summary>
    public float yVelocity = 0;

    /// <summary>
    /// �÷��̾� ������
    /// </summary>
    public float jumpPower = 5.0f;

    /// <summary>
    /// ���� ���� ����
    /// </summary>
    public bool Isjumping = false;

    /// <summary>
    /// �÷��̾� ü��
    /// </summary>
    public int hp = 50;

    /// <summary>
    /// �÷��̾� �ִ� ü��
    /// </summary>
    public int Maxhp = 50;

    /// <summary>
    /// hp �����̴�
    /// </summary>
    public Slider slider;

    /// <summary>
    /// Hit ȿ�� ������Ʈ
    /// </summary>
    public GameObject hitEffect;


    Animator animator;

    private void Start()
    {
        cc = GetComponent<CharacterController>();

        animator = GetComponentInChildren<Animator>();
    }


    private void Update()
    {
        // ���� ���°� �������϶��� ���� ����
        if (GameManager.gm.state != GameManager.State.Play)
        {
            return;
        }

        // W, A, S, D�� �����̰� �����
        // �÷��̾��� �Է� �ޱ�
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // �̵� ���� ����
        Vector3 dir = new Vector3(h, 0, v);
        dir = dir.normalized;

        animator.SetFloat("MoveMotion", dir.magnitude);

        // ���� ī�޶� �������� ���� ��ȯ
        dir = Camera.main.transform.TransformDirection(dir);

        // ĳ���� ���� �ӵ��� �߷°� ����
        yVelocity += gravity * Time.deltaTime;
        dir.y = yVelocity;

        // �̵��ӵ��� ���� �̵�
        cc.Move(dir * MoveSpeed * Time.deltaTime);

        // ���� ���̾���, �ٽ� �ٴڿ� �����ϸ� ����
        if(cc.collisionFlags == CollisionFlags.Below)
        {
            // �������̶�� ����
            if (Isjumping)
            {
                // ���� �� ���·� �ʱ�ȭ
                Isjumping = false;
                // ���� �ӵ��� 0���� �Ѵ�.
                yVelocity = 0;
            }
        }

        // �����̽� Ű�� ������ �����ϱ�
        if (Input.GetButtonDown("Jump") && !Isjumping)
        {
            // �÷��̾� ���� �ӵ��� �������� ����
            yVelocity = jumpPower;
            Isjumping = true;
        }

        // �÷��̾��� ü�¹ٴ� hp / maxhp�� ���·� ��Ÿ����.
        slider.value = (float)hp / (float)Maxhp;

    }

    /// <summary>
    /// �÷��̾��� �ǰ� �Լ�
    /// </summary>
    /// <param name="damage">�÷��̾ �޴� ������</param>
    public void DamageAction(int damage)
    {
        // ���ʹ��� ���ݷ¸�ŭ �÷��̾��� ü�� ���
        hp -= damage;

        // �÷��̾�M ü���� 0 �̻��� ��
        if(hp > 0)
        {
            // �������� ������ PlayerHit()�Լ� ����
            StartCoroutine(PlayerHit());
        }
    }

    /// <summary>
    /// �÷��̾� �ǰ� ����Ʈ ���� �Լ�
    /// </summary>
    /// <returns></returns>
    IEnumerator PlayerHit()
    {
        // 1. ����Ʈ Ȱ��ȭ
        hitEffect.SetActive(true);

        // 2. ����Ʈ Ȱ��ȭ �� 0.3�ʰ� ������
        yield return new WaitForSeconds(0.3f);

        // 3. ����Ʈ ��Ȱ��ȭ
        hitEffect.SetActive(false);
    }
}

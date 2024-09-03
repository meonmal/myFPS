using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public bool Isjumping = false;

    private void Start()
    {
        cc = GetComponent<CharacterController>();
    }


    private void Update()
    {
        // W, A, S, D�� �����̰� �����
        // �÷��̾��� �Է� �ޱ�
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // �̵� ���� ����
        Vector3 dir = new Vector3(h, 0, v);
        dir = dir.normalized;

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
    }
}

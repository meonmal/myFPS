using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    /// <summary>
    /// �̵� �ӵ�
    /// </summary>
    public float MoveSpeed = 7.0f;

    private void Update()
    {
        // W, A, S, D�� �����̰� �����

        // �÷��̾��� �Է� �ޱ�
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // �̵� ���� ����
        Vector3 dir = new Vector3(h, 0, v);
        dir = dir.normalized;

        dir = Camera.main.transform.TransformDirection(dir);

        // �̵� ����
        transform.position += dir * MoveSpeed * Time.deltaTime;
    }
}

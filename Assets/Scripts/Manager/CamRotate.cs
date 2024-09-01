using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotate : MonoBehaviour
{
    // ȸ�� �ӵ�
    public float RotateSpeed = 360.0f;

    public float mouse_x = 0.0f;
    public float mouse_y = 0.0f;


    private void Update()
    {
        // ���콺�� �Է��� �޾� ��ü ȸ���ϱ�
        
        // ���콺�� �Է��� �޴´�
        float MouseX = Input.GetAxis("Mouse X");
        float MouseY = Input.GetAxis("Mouse Y");

        // ȸ���� ������ ���콺 �Է� ����ŭ �̸� ����
        mouse_x += MouseX * RotateSpeed * Time.deltaTime;
        mouse_y += MouseY * RotateSpeed * Time.deltaTime;

        // ���콺 ���� �̵� ����(mouse_y)�� ���� -90 ~ 90���� ����
        mouse_y = Mathf.Clamp(mouse_y, -90, 90);

        // ��ü ȸ��
        transform.eulerAngles = new Vector3(-mouse_y, mouse_x, 0);
    }
}

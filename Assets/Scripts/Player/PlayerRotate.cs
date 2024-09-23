using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    // ȸ�� �ӵ�
    public float RotateSpeed = 360.0f;

    public float mouse_x = 0.0f;
   


    private void Update()
    {
        // ���� ���°� �������϶��� ���� ����
        if(GameManager.gm.state != GameManager.State.Play)
        {
            return;
        }

        // ���콺�� �Է��� �޴´�
        float MouseX = Input.GetAxis("Mouse X");
        
        // ȸ���� ������ ���콺 �Է� ����ŭ �̸� ����
        mouse_x += MouseX * RotateSpeed * Time.deltaTime;
       
        // ��ü ȸ��
        transform.eulerAngles = new Vector3(0, mouse_x, 0);
    }
}

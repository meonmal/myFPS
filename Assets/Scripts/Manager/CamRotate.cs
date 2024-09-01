using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotate : MonoBehaviour
{
    // 회전 속도
    public float RotateSpeed = 360.0f;

    public float mouse_x = 0.0f;
    public float mouse_y = 0.0f;


    private void Update()
    {
        // 마우스의 입력을 받아 물체 회전하기
        
        // 마우스의 입력을 받는다
        float MouseX = Input.GetAxis("Mouse X");
        float MouseY = Input.GetAxis("Mouse Y");

        // 회전값 변수에 마우스 입력 값만큼 미리 누적
        mouse_x += MouseX * RotateSpeed * Time.deltaTime;
        mouse_y += MouseY * RotateSpeed * Time.deltaTime;

        // 마우스 상하 이동 변수(mouse_y)의 값을 -90 ~ 90도로 제한
        mouse_y = Mathf.Clamp(mouse_y, -90, 90);

        // 물체 회전
        transform.eulerAngles = new Vector3(-mouse_y, mouse_x, 0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    // 회전 속도
    public float RotateSpeed = 360.0f;

    public float mouse_x = 0.0f;
   


    private void Update()
    {
        // 마우스의 입력을 받아 물체 회전하기

        // 마우스의 입력을 받는다
        float MouseX = Input.GetAxis("Mouse X");
        
        // 회전값 변수에 마우스 입력 값만큼 미리 누적
        mouse_x += MouseX * RotateSpeed * Time.deltaTime;
       
        // 물체 회전
        transform.eulerAngles = new Vector3(0, mouse_x, 0);
    }
}

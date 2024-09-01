using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    /// <summary>
    /// 이동 속도
    /// </summary>
    public float MoveSpeed = 7.0f;

    private void Update()
    {
        // W, A, S, D로 움직이게 만들기

        // 플레이어의 입력 받기
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // 이동 방향 설정
        Vector3 dir = new Vector3(h, 0, v);
        dir = dir.normalized;

        dir = Camera.main.transform.TransformDirection(dir);

        // 이동 설정
        transform.position += dir * MoveSpeed * Time.deltaTime;
    }
}

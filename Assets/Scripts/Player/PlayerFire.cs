using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    /// <summary>
    /// 발사 위치
    /// </summary>
    public GameObject firePosition;

    /// <summary>
    /// 투척 무시 오브젝트
    /// </summary>
    public GameObject Bomb;

    /// <summary>
    /// 투척 파워
    /// </summary>
    public float throwPower = 15.0f;

    public void Update()
    {
        // 마우스의 오른쪽 버튼을 누르면 실행
        if (Input.GetMouseButton(1))
        {
            // 수류탄 오브젝트 생성 후
            GameObject bomb = Instantiate(Bomb);

            // 수류탄의 생성 위치를 발사 위치로 한다.
            bomb.transform.position = firePosition.transform.position;

            // 수류탄 오브젝트의 RIgidBody 컴포넌트를 가져온다.
            Rigidbody rb = bomb.GetComponent<Rigidbody>();

            // 카메라의 정면 방향으로 수류탄에 물리적인 힘을 가한다.
            rb.AddForce(Camera.main.transform.forward * throwPower, ForceMode.Impulse);
        }
    }
}

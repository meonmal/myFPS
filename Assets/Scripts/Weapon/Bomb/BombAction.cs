using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAction : MonoBehaviour
{
    /// <summary>
    /// 폭팔 이팩트 프리팹
    /// </summary>
    public GameObject bombEffect;

    // 충돌했을 때 실행
    public void OnCollisionEnter(Collision collision)
    {
        // 이펙트 프리팹 생성
        GameObject eff = Instantiate(bombEffect);

        // 이펙트 프리팹의 위치는 수류탄 오브젝트 자신의 위치와 동일하다.
        eff.transform.position = transform.position;

        // 자기 자신을 제거한다.
        Destroy(gameObject);
    }
}

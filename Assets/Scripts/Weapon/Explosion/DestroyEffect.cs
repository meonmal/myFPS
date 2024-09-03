using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEffect : MonoBehaviour
{
    /// <summary>
    /// 제거될 시간
    /// </summary>
    public float destroyTime = 1.5f;

    /// <summary>
    /// 경과 시간 측정
    /// </summary>
    public float currentTime = 0;

    public void Update()
    {
        // 경과 시간이 제거될 시간을 초과하면 실행
        if(currentTime > destroyTime)
        {
            // 자기 자신 제거
            Destroy(gameObject);
        }

        // 경과 시간을 누적한다.
        currentTime += Time.deltaTime;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEffect : MonoBehaviour
{
    /// <summary>
    /// ���ŵ� �ð�
    /// </summary>
    public float destroyTime = 1.5f;

    /// <summary>
    /// ��� �ð� ����
    /// </summary>
    public float currentTime = 0;

    public void Update()
    {
        // ��� �ð��� ���ŵ� �ð��� �ʰ��ϸ� ����
        if(currentTime > destroyTime)
        {
            // �ڱ� �ڽ� ����
            Destroy(gameObject);
        }

        // ��� �ð��� �����Ѵ�.
        currentTime += Time.deltaTime;
    }
}

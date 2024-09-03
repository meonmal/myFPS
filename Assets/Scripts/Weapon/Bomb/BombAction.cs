using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAction : MonoBehaviour
{
    /// <summary>
    /// ���� ����Ʈ ������
    /// </summary>
    public GameObject bombEffect;

    // �浹���� �� ����
    public void OnCollisionEnter(Collision collision)
    {
        // ����Ʈ ������ ����
        GameObject eff = Instantiate(bombEffect);

        // ����Ʈ �������� ��ġ�� ����ź ������Ʈ �ڽ��� ��ġ�� �����ϴ�.
        eff.transform.position = transform.position;

        // �ڱ� �ڽ��� �����Ѵ�.
        Destroy(gameObject);
    }
}

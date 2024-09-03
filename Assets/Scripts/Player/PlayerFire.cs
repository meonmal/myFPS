using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    /// <summary>
    /// �߻� ��ġ
    /// </summary>
    public GameObject firePosition;

    /// <summary>
    /// ��ô ���� ������Ʈ
    /// </summary>
    public GameObject Bomb;

    /// <summary>
    /// ��ô �Ŀ�
    /// </summary>
    public float throwPower = 15.0f;

    public void Update()
    {
        // ���콺�� ������ ��ư�� ������ ����
        if (Input.GetMouseButton(1))
        {
            // ����ź ������Ʈ ���� ��
            GameObject bomb = Instantiate(Bomb);

            // ����ź�� ���� ��ġ�� �߻� ��ġ�� �Ѵ�.
            bomb.transform.position = firePosition.transform.position;

            // ����ź ������Ʈ�� RIgidBody ������Ʈ�� �����´�.
            Rigidbody rb = bomb.GetComponent<Rigidbody>();

            // ī�޶��� ���� �������� ����ź�� �������� ���� ���Ѵ�.
            rb.AddForce(Camera.main.transform.forward * throwPower, ForceMode.Impulse);
        }
    }
}

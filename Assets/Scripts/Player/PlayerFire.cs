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

    /// <summary>
    /// �ǰ� ����Ʈ ������Ʈ
    /// </summary>
    public GameObject bulletEffect;

    /// <summary>
    /// �ǰ� ����Ʈ ��ƼŬ �ý���
    /// </summary>
    ParticleSystem ps;


    /// <summary>
    /// �߻� ���� ���ݷ�
    /// </summary>
    public int weaponPower = 5;


    Animator animator;

    public void Start()
    {
        // �ǰ� ����Ʈ ������Ʈ���� ��ƼŬ �ý��� ������Ʈ ��������
        ps = bulletEffect.GetComponent<ParticleSystem>();

        animator = GetComponentInChildren<Animator>();
    }


    public void Update()
    {
        // ���� ���°� �������϶��� ���� ����
        if (GameManager.gm.state != GameManager.State.Play)
        {
            return;
        }

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
        // ���콺�� ���� ��ư�� ������ ����
        if (Input.GetMouseButton(0))
        {
            if(animator.GetFloat("MoveMotion") == 0)
            {
                animator.SetTrigger("Attack");
            }
            
            
            // ���̸� ������ �� �߻�� ��ġ�� ���� ������ �����Ѵ�.
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            
            // ���̰� �ε��� ����� ������ ������ ������ �����Ѵ�.
            RaycastHit hit = new RaycastHit();



            // ���̸� �߻��� �� �ε��� ��ü�� ������ �ǰ� ����Ʈ�� ǥ���Ѵ�.
            if(Physics.Raycast(ray, out hit))
            {
                // ���̿� �ε��� ����� ���̾ Enemy��� �����Ѵ�.
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    // ������ �Լ�
                    Enemy enemy = hit.transform.GetComponent<Enemy>();
                    enemy.HitEnemy(weaponPower);
                }
                // ���� �ƴ϶�� ����
                else
                {
                    // �ǰ� ����Ʈ�� ��ġ�� ���̰� �ε��� �������� �̵���Ų��.
                    bulletEffect.transform.position = hit.point;

                    bulletEffect.transform.forward = hit.normal;

                    // �ǰ� ����Ʈ�� �÷����Ѵ�.
                    ps.Play();
                }
            }
        }
    }
}

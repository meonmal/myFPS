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

    /// <summary>
    /// 피격 이펙트 오브젝트
    /// </summary>
    public GameObject bulletEffect;

    /// <summary>
    /// 피격 이펙트 파티클 시스템
    /// </summary>
    ParticleSystem ps;


    /// <summary>
    /// 발사 무기 공격력
    /// </summary>
    public int weaponPower = 5;


    Animator animator;

    public void Start()
    {
        // 피격 이펙트 오브젝트에서 파티클 시스템 컴포넌트 가져오기
        ps = bulletEffect.GetComponent<ParticleSystem>();

        animator = GetComponentInChildren<Animator>();
    }


    public void Update()
    {
        // 게임 상태가 게임중일때만 조작 가능
        if (GameManager.gm.state != GameManager.State.Play)
        {
            return;
        }

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
        // 마우스의 왼쪽 버튼을 누르면 실행
        if (Input.GetMouseButton(0))
        {
            if(animator.GetFloat("MoveMotion") == 0)
            {
                animator.SetTrigger("Attack");
            }
            
            
            // 레이를 생성한 후 발사될 위치와 진행 방향을 설정한다.
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            
            // 레이가 부딪힌 대상의 정보를 저장할 변수를 생성한다.
            RaycastHit hit = new RaycastHit();



            // 레이를 발사한 후 부딪힌 물체가 있으면 피격 이펙트를 표시한다.
            if(Physics.Raycast(ray, out hit))
            {
                // 레이에 부딪힌 대상의 레이어가 Enemy라면 실행한다.
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    // 데미지 함수
                    Enemy enemy = hit.transform.GetComponent<Enemy>();
                    enemy.HitEnemy(weaponPower);
                }
                // 적이 아니라면 실행
                else
                {
                    // 피격 이펙트의 위치를 레이가 부딪힌 지점으로 이동시킨다.
                    bulletEffect.transform.position = hit.point;

                    bulletEffect.transform.forward = hit.normal;

                    // 피격 이펙트를 플레이한다.
                    ps.Play();
                }
            }
        }
    }
}

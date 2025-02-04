using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    /// <summary>
    /// 이동 속도
    /// </summary>
    public float MoveSpeed = 7.0f;

    /// <summary>
    /// 플레이어 컨트롤러
    /// </summary>
    public CharacterController cc;

    /// <summary>
    /// 플레이어 중력
    /// </summary>
    public float gravity = -20.0f;

    /// <summary>
    /// 플레이어 수직 속력
    /// </summary>
    public float yVelocity = 0;

    /// <summary>
    /// 플레이어 점프력
    /// </summary>
    public float jumpPower = 5.0f;

    /// <summary>
    /// 현재 점프 상태
    /// </summary>
    public bool Isjumping = false;

    /// <summary>
    /// 플레이어 체력
    /// </summary>
    public int hp = 50;

    /// <summary>
    /// 플레이어 최대 체력
    /// </summary>
    public int Maxhp = 50;

    /// <summary>
    /// hp 슬라이더
    /// </summary>
    public Slider slider;

    /// <summary>
    /// Hit 효과 오브젝트
    /// </summary>
    public GameObject hitEffect;


    Animator animator;

    private void Start()
    {
        cc = GetComponent<CharacterController>();

        animator = GetComponentInChildren<Animator>();
    }


    private void Update()
    {
        // 게임 상태가 게임중일때만 조작 가능
        if (GameManager.gm.state != GameManager.State.Play)
        {
            return;
        }

        // W, A, S, D로 움직이게 만들기
        // 플레이어의 입력 받기
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // 이동 방향 설정
        Vector3 dir = new Vector3(h, 0, v);
        dir = dir.normalized;

        animator.SetFloat("MoveMotion", dir.magnitude);

        // 메인 카메라 기준으로 방향 변환
        dir = Camera.main.transform.TransformDirection(dir);

        // 캐릭터 수직 속도에 중력값 적용
        yVelocity += gravity * Time.deltaTime;
        dir.y = yVelocity;

        // 이동속도에 맞춰 이동
        cc.Move(dir * MoveSpeed * Time.deltaTime);

        // 점프 중이었고, 다시 바닥에 착지하면 실행
        if(cc.collisionFlags == CollisionFlags.Below)
        {
            // 점프중이라면 실행
            if (Isjumping)
            {
                // 점프 전 상태로 초기화
                Isjumping = false;
                // 수직 속도를 0으로 한다.
                yVelocity = 0;
            }
        }

        // 스페이스 키를 누르면 점프하기
        if (Input.GetButtonDown("Jump") && !Isjumping)
        {
            // 플레이어 수직 속도에 점프력을 적용
            yVelocity = jumpPower;
            Isjumping = true;
        }

        // 플레이어의 체력바는 hp / maxhp의 상태로 나타난다.
        slider.value = (float)hp / (float)Maxhp;

    }

    /// <summary>
    /// 플레이어의 피격 함수
    /// </summary>
    /// <param name="damage">플레이어가 받는 데미지</param>
    public void DamageAction(int damage)
    {
        // 에너미의 공격력만큼 플레이어의 체력 깎기
        hp -= damage;

        // 플레이어츼 체력이 0 이상일 때
        if(hp > 0)
        {
            // 데미지를 받으면 PlayerHit()함수 실행
            StartCoroutine(PlayerHit());
        }
    }

    /// <summary>
    /// 플레이어 피격 이펙트 실행 함수
    /// </summary>
    /// <returns></returns>
    IEnumerator PlayerHit()
    {
        // 1. 이펙트 활성화
        hitEffect.SetActive(true);

        // 2. 이펙트 활성화 후 0.3초가 지나면
        yield return new WaitForSeconds(0.3f);

        // 3. 이펙트 비활성화
        hitEffect.SetActive(false);
    }
}

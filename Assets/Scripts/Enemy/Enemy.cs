using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    /// <summary>
    /// 적이 가질 수 있는 상태
    /// </summary>
    enum EnemyState
    {
        Idle,
        Move,
        Attack,
        Return,
        Damage,
        Die,
    }

    /// <summary>
    /// 적의 현재 상태
    /// </summary>
    EnemyState m_State;

    /// <summary>
    /// 플레이어 발견 범위
    /// </summary>
    public float findDistance = 8.0f;

    /// <summary>
    /// 플레이어 트랜스폼
    /// </summary>
    Transform player;

    /// <summary>
    /// 공격 가능 범위
    /// </summary>
    public float attackDistance = 2.0f;

    /// <summary>
    /// 적 이동속도
    /// </summary>
    public float moveSpeed = 5.0f;

    /// <summary>
    /// 캐릭터 컨트롤러 컴포넌트
    /// </summary>
    CharacterController cc;

    /// <summary>
    /// 누적 시간
    /// </summary>
    public float currentTime = 0.0f;

    /// <summary>
    /// 공격 딜레이 시간
    /// </summary>
    public float attackDelay = 2.0f;

    /// <summary>
    /// 에너미의 공격력
    /// </summary>
    public int attackPower = 3;

    /// <summary>
    /// 초기 위치 저장용
    /// </summary>
    Vector3 originposition;

    /// <summary>
    /// 이동 가능 범위
    /// </summary>
    public float moveDistance = 20.0f;


    /// <summary>
    /// 적의 체력
    /// </summary>
    public float hp = 15.0f;

    /// <summary>
    /// 적의 최대 hp
    /// </summary>
    public float maxhp = 15.0f;

    /// <summary>
    /// 적의 발사 무기 공격력
    /// </summary>
    public int weponPower = 5;


    private void Start()
    {
        // 적이 처음에 하는 행동은 대기다.
        m_State = EnemyState.Idle;

        // 플레이어의 트랜스폼 컴포넌트 찾기
        player = GameObject.Find("Player").transform;

        // 캐릭터 컴포넌트 받아오기
        cc = GetComponent<CharacterController>();

        // 적의 초기 위치 저장
        originposition = transform.position;
    }

    private void Update()
    {
        // 현재 상태를 체크하여 해당 상태별로 특정 기능을 수행하게 만들기.
        switch (m_State)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Move:
                Move();
                break;
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Return:
                Return();
                break;
            case EnemyState.Damage:
                //Demage();
                break;
            case EnemyState.Die:
                //Die();
                break;
        }
    }

    public void Idle()
    {
        // 플레이어가 발견범위 안에 있으면 실행
        if(Vector3.Distance(transform.position, player.position) < findDistance)
        {
            m_State = EnemyState.Move;
            print("상태 전환 Idle -> Move");
        }
    }
    public void Move()
    {
        // 현재 위치가 초기 위치에서 이동 가능 범위를 넘어간다면 실행
        if (Vector3.Distance(transform.position, originposition) > moveDistance)
        {
            // 현재 상태를 Return으로 복귀
            m_State = EnemyState.Return;
            print("상태 전환 : Move -> Return");
        }
        // 플레이어가 공격 범위 밖에 있다면 실행
        else if(Vector3.Distance(transform.position, player.position) > attackDistance)
        {   
            Vector3 dir = (player.position - transform.position).normalized;

            cc.Move(dir * moveSpeed * Time.deltaTime);
        }
        // 위의 경우가 둘 다 아니라면 실행
        else
        {
            // 적의 상태를 Attack으로 변환
            m_State = EnemyState.Attack;
            print("상태 전환 : Move -> Attack");
        }
    }

    public void Attack()
    {
        // 플레이어가 공격 범위 안이라면 플레이어 공격
        if(Vector3.Distance(transform.position, player.position) < attackDistance)
        {
            // 일정 시간마다 플레이어 공격
            currentTime += Time.deltaTime;

            if(currentTime > attackDelay)
            {
                player.GetComponent<PlayerMove>().DamageAction(attackPower);
                print("공격");
                currentTime = 0;

            }
        }
        // 그렇지 않다면 실행
        else
        {
            // 현재 상태 변경(재추격 실기)
            m_State = EnemyState.Move;
            print("상태전환 Attack -> Move");
            currentTime = 0;
        }
    }

    public void Return()
    {
        // 초기 위치에서의 거리가 0.1f 이상이면 실행
        if(Vector3.Distance(transform.position , originposition) > 0.1f)
        {
            // 초기 위치 쪽으로 이동
            Vector3 dir = (originposition - transform.position).normalized;
            cc.Move(dir * moveSpeed * Time.deltaTime);
        }
        // 그렇지 않다면 실행
        else
        {
            // 적의 위치 초기화
            transform.position = originposition;
            // hp를 다시 회복한다.
            hp = maxhp;
            // 적의 상태를 전환한다.
            m_State = EnemyState.Idle;
            print("상태 전환 Return -> Idle");
        }
    }

    public void Damage()
    {
        // 피격 상태를 처리하기 위한 코루틴
        StartCoroutine(DamageProcess());
    }

    /// <summary>
    /// 데미지 처리용 코루틴 함수
    /// </summary>
    /// <returns></returns>
    IEnumerator DamageProcess()
    {
        // 피격 모션 시간만큼 기다린다.
        yield return new WaitForSeconds(0.5f);

        // 현재 상태를 이동 상태로 전환
        m_State = EnemyState.Move;
        print("상태 전환 Demaged -> Move");
    }

    public void Die()
    {
        StopAllCoroutines();

        StartCoroutine(DieProcess());
    }

    private IEnumerator DieProcess()
    {
        // 캐릭터 컴포넌트를 비활성화 시키기
        cc.enabled = false;

        // 2초후 소멸
        yield return new WaitForSeconds(2f);
        print("소멸");
        Destroy(gameObject);
    }

    /// <summary>
    /// 데미지 실행 함수
    /// </summary>
    /// <param name="hitPower"></param>
    public void HitEnemy(int hitPower)
    {
        // 적이 피격중이거나 죽은 상태이거나 돌아가는 상태일 경우 데미지를 받지 않음.
        if (m_State == EnemyState.Damage || m_State == EnemyState.Die || m_State == EnemyState.Return)
        {
            return;
        }

        // 플레이어의 공격력만큼 체력 깎기
        hp -= hitPower;

        // 체력이 0 이상이라면 실행
        if(hp > 0)
        {
            // 상태를 Attack으로 변경
            m_State = EnemyState.Attack;
            print("상태 전환 Any state -> Damaged");
            Damage();
        }
        // 그렇지 않다면 죽음상태로 전환
        else
        {
            m_State = EnemyState.Die;
            print("상태 전환 any state -> Die");
            Die();
        }
    }

    
}

using System;
using System.Collections;
using System.Collections.Generic;
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

    private void Start()
    {
        // 적이 처음에 하는 행동은 대기다.
        m_State = EnemyState.Idle;

        // 플레이어의 트랜스폼 컴포넌트 찾기
        player = GameObject.Find("Player").transform;

        // 캐릭터 컴포넌트 받아오기
        cc = GetComponent<CharacterController>();
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
                Demage();
                break;
            case EnemyState.Die:
                Die();
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
        // 플레이어가 공격 범위 밖에 있다면 실행
        if(Vector3.Distance(transform.position, player.position) > attackDistance)
        {
            // 이동 방향 설정
            Vector3 dir = (player.position - transform.position).normalized;

            // 캐릭터 컨트롤러를 이용해 이동하기
            cc.Move(dir * moveSpeed * Time.deltaTime);
        }
        // if의 경우가 아니면 실행
        else
        {
            m_State = EnemyState.Attack;
            print("상태 전환 Move -> Attack");
        }
    }

    public void Attack()
    {
        if(Vector3.Distance(transform.position, player.position) < attackDistance)
        {
            currentTime += Time.deltaTime;
            if(currentTime > attackDelay)
            {
                print("공격");
                currentTime = 0;
            }
        }
    }

    public void Return()
    {

    }

    public void Demage()
    {

    }

    public void Die()
    {

    }


}

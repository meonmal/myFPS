using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// 싱글톤 변수
    /// </summary>
    public static GameManager gm;

    /// <summary>
    /// 게임 상태 오브젝트
    /// </summary>
    public GameObject gameLabel;

    /// <summary>
    /// 게임상태 텍스트
    /// </summary>
    TextMeshProUGUI gametext;

    /// <summary>
    /// PlayerMove 클래스 변수
    /// </summary>
    PlayerMove player;

    private void Awake()
    {
        if(gm == null)
        {
            gm = this;
        }
    }

    public enum State
    {
        Ready,
        Play,
        GameOver
    }

    /// <summary>
    /// 현재의 플레이 상태 변수
    /// </summary>
    public State state;

    private void Start()
    {
        // 초기상태는 준비상태
        state = State.Ready;

        // 게임 상태 오브젝트에서 text 컴포넌트를 가져온다.
        gametext = gameLabel.GetComponent<TextMeshProUGUI>();

        // 상태 텍스트의 내용을 Ready...로 한다.
        gametext.text = "Ready...";

        // 현재 텍스트의 색(거의 주황색(?))
        gametext.color = new Color(255, 185, 0, 255);

        // 준비에서 게임중 상태로 변환
        StartCoroutine(ReadyToStart());

        player = GameObject.Find("Player").GetComponent<PlayerMove>();
    }

    IEnumerator ReadyToStart()
    {
        // 2초간 대기
        yield return new WaitForSeconds(2.0f);

        // 2초가 지나면 텍스트를 Go!로 바꾼다.
        gametext.text = "Go!";

        // 다시 0.5초가 지나면
        yield return new WaitForSeconds(0.5f);

        // 게임라벨을 비활성화 시킨다.
        gameLabel.SetActive(false);

        // 그 후 상태는 플레이 상태다.
        state = State.Play;
    }

    private void Update()
    {
        // 플레이어의 체력이 0 이하가 되면
        if(player.hp <= 0)
        {
            // 다시 상태 텍스트를 활성화 시키고
            gameLabel.SetActive(true);

            // 텍스트는 Game Over로 한다.
            gametext.text = "Game Over";

            // 텍스트의 색깔은 빨강
            gametext.color = new Color32(255, 0, 0, 255);

            // 상태는 게임오버로 한다.
            state = State.GameOver;
        }
    }
}

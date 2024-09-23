using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// �̱��� ����
    /// </summary>
    public static GameManager gm;

    /// <summary>
    /// ���� ���� ������Ʈ
    /// </summary>
    public GameObject gameLabel;

    /// <summary>
    /// ���ӻ��� �ؽ�Ʈ
    /// </summary>
    TextMeshProUGUI gametext;

    /// <summary>
    /// PlayerMove Ŭ���� ����
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
    /// ������ �÷��� ���� ����
    /// </summary>
    public State state;

    private void Start()
    {
        // �ʱ���´� �غ����
        state = State.Ready;

        // ���� ���� ������Ʈ���� text ������Ʈ�� �����´�.
        gametext = gameLabel.GetComponent<TextMeshProUGUI>();

        // ���� �ؽ�Ʈ�� ������ Ready...�� �Ѵ�.
        gametext.text = "Ready...";

        // ���� �ؽ�Ʈ�� ��(���� ��Ȳ��(?))
        gametext.color = new Color(255, 185, 0, 255);

        // �غ񿡼� ������ ���·� ��ȯ
        StartCoroutine(ReadyToStart());

        player = GameObject.Find("Player").GetComponent<PlayerMove>();
    }

    IEnumerator ReadyToStart()
    {
        // 2�ʰ� ���
        yield return new WaitForSeconds(2.0f);

        // 2�ʰ� ������ �ؽ�Ʈ�� Go!�� �ٲ۴�.
        gametext.text = "Go!";

        // �ٽ� 0.5�ʰ� ������
        yield return new WaitForSeconds(0.5f);

        // ���Ӷ��� ��Ȱ��ȭ ��Ų��.
        gameLabel.SetActive(false);

        // �� �� ���´� �÷��� ���´�.
        state = State.Play;
    }

    private void Update()
    {
        // �÷��̾��� ü���� 0 ���ϰ� �Ǹ�
        if(player.hp <= 0)
        {
            // �ٽ� ���� �ؽ�Ʈ�� Ȱ��ȭ ��Ű��
            gameLabel.SetActive(true);

            // �ؽ�Ʈ�� Game Over�� �Ѵ�.
            gametext.text = "Game Over";

            // �ؽ�Ʈ�� ������ ����
            gametext.color = new Color32(255, 0, 0, 255);

            // ���´� ���ӿ����� �Ѵ�.
            state = State.GameOver;
        }
    }
}

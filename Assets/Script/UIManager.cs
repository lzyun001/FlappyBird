using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public GameObject Player;
    public Text Text_Start;
    public Text Text_PlayerScore;
    public Text Text_ScoreEnd;
    public Button Btn_Quit;
    public Button Btn_Restart;
    public GameObject ScoreBoard;
    public GameManager gameManager;
    private IEnumerator coroutine;

    private void Awake()
    {
        Btn_Quit.onClick.AddListener(BtnClick_Quit);
        Btn_Restart.onClick.AddListener(BtnClick_Restart);
        Init();
    }

    private void Init()
    {
        Text_Start.enabled = true;
        coroutine = TextFade(0.75f);
        StartCoroutine(coroutine);
    }
    
    public IEnumerator TextFade(float fadeTimer)
    {
        while (true)
        {
            Text_Start.DOFade(150f / 255f, fadeTimer);
            yield return new WaitForSeconds(fadeTimer);
            Text_Start.DOFade(255 / 255, fadeTimer);
            yield return new WaitForSeconds(fadeTimer);
        }
    }

    private void BtnClick_Quit()
    {
        Application.Quit();
    }

    private void BtnClick_Restart()
    {
        Init();
        ScoreBoard.SetActive(false);
        Player.GetComponent<PlayerControl>().Restart();
        gameManager.DestroyAllBlocks();
    }
}

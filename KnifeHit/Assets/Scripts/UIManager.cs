using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices.WindowsRuntime;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private Text scoreText;
    [SerializeField] private Text stageText;
    [SerializeField] private Text appleText;
    [SerializeField] private GameObject stageContainer;

    [SerializeField] private Color stageCompletedColor;
    [SerializeField] private Color stageNormalColor;
    public List<Image> stageIcons;

    [Header ("UI Background")]
    [SerializeField] private Image defaultBackground;
    [SerializeField] private Sprite firstBackground;
    [SerializeField] private Sprite secondBackground;
    [SerializeField] private Sprite thirdBackground;
    [SerializeField] private Sprite fourBackground;

    [SerializeField] private Sprite fiveBackground;
    [SerializeField] private Sprite sixBackground;
    [SerializeField] private Sprite sevenBackground;
    [SerializeField] private Sprite eightBackground;


    [Header (header: "UI BOSS")]
    [SerializeField] private GameObject bossFight;
    [SerializeField] private GameObject bossDefeated;

    [Header (header: "GameOver UI")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Text gameOverScore;
    [SerializeField] private Text gameOverStage;

    [Header ("Victory UI")]
    [SerializeField] private GameObject victoryPannel;
    [SerializeField] private Text victoryScore;
    [SerializeField] private Text victoryStage;

    private void Awake()
    {
        if(Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void UIbackground()
    {
        if(GameManager.Instance.Stage < 5)
        {
            defaultBackground.sprite = firstBackground;
        }
        else if (GameManager.Instance.Stage > 5 && GameManager.Instance.Stage < 10)
        {
            defaultBackground.sprite = secondBackground;
        }
        else if(GameManager.Instance.Stage > 10 && GameManager.Instance.Stage < 15)
        {
            defaultBackground.sprite  = thirdBackground;
        }
        else if (GameManager.Instance.Stage > 15 && GameManager.Instance.Stage < 20)
        {
            defaultBackground.sprite = fourBackground;
        }
        else if(GameManager.Instance.Stage > 20 && GameManager.Instance.Stage < 25)
        {
            defaultBackground.sprite  = fiveBackground;
        }
        else if (GameManager.Instance.Stage > 25 && GameManager.Instance.Stage < 30)
        {
            defaultBackground.sprite = sixBackground;
        }
        else if(GameManager.Instance.Stage > 30 && GameManager.Instance.Stage < 35)
        {
            defaultBackground.sprite  = sevenBackground;
        }
        else if(GameManager.Instance.Stage > 35)
        {
            defaultBackground.sprite = eightBackground;
        }  
    }

    private void Update()
    {
        UIbackground();
        scoreText.text = GameManager.Instance.Score.ToString();
        gameOverScore.text = GameManager.Instance.Score.ToString();
        victoryScore.text = GameManager.Instance.Score.ToString();
        appleText.text = GameManager.Instance.TotalApple.ToString();

        stageText.text = "Stage " + GameManager.Instance.Stage;
        gameOverStage.text = "Stage " + GameManager.Instance.Stage;

        stageText.text = "Stage " + GameManager.Instance.Stage;
        victoryStage.text = "Stage " + GameManager.Instance.Stage;

        UpdateUI();
    }

    public IEnumerator BossStart()
    {
        SoundManager.Instance.PlayBossStartFight();
        bossFight.SetActive(true);
        yield return new WaitForSeconds(1f);
        bossFight.SetActive(false);
    }

    public IEnumerator BossDefeated()
    {
        SoundManager.Instance.PlayBossEndFight();
        bossDefeated.SetActive(true);
        yield return new WaitForSeconds(1f);
        bossDefeated.SetActive(false);
    }

    public void GameOver()
    {
        if(GameManager.Instance.Score > GameManager.Instance.HighScore)
        {
            GameManager.Instance.HighScore = GameManager.Instance.Score;
        }
        SoundManager.Instance.PlayGameOver();
        gameOverPanel.SetActive(true);
        stageContainer.SetActive(false);
    }

    public void Victory()
    {
        if(GameManager.Instance.Score > GameManager.Instance.HighScore)
        {
            GameManager.Instance.HighScore = GameManager.Instance.Score;
        }
        SoundManager.Instance.PlayVictory();
        victoryPannel.SetActive(true);
        stageContainer.SetActive(false);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Home()
    {
        SceneManager.LoadScene("Menu");
    }

    public void OpenShop()
    {
        GeneralUI.Instance.openShop();
    }

    public void OpenSettings()
    {
        GeneralUI.Instance.openSettings();
    }

    public void UpdateUI()
    {
        if(GameManager.Instance.Stage % 5 == 0)
        {
            foreach(var icon in stageIcons)
            {
                icon.gameObject.SetActive(false);
                stageIcons[stageIcons.Count - 1].color = stageNormalColor;
                stageText.text = LevelManager.Instance.BossName;
            }
        }  
        else
        {
            for(int i = 0; i < stageIcons.Count; i++)
            {
                stageIcons[i].gameObject.SetActive(true);
                stageIcons[i].color = GameManager.Instance.Stage % 5 <= i ? stageNormalColor : stageCompletedColor;
            }
        }
    }
}

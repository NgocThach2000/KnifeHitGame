using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices.WindowsRuntime;

public class GeneralUI : MonoBehaviour
{
    public static GeneralUI Instance;

    [Header("Panels")]
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject shopPanel;

    [Header("UI")]
    [SerializeField] private GameObject soundsOn;
    [SerializeField] private GameObject soundsOff;
    [SerializeField] private GameObject vibrateOn;
    [SerializeField] private GameObject vibrateOff;
    [SerializeField] private Text totalApple;
    [SerializeField] private Text highScore;

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
    private void Update()
    {
        totalApple.text = GameManager.Instance.TotalApple.ToString();
        highScore.text = GameManager.Instance.HighScore.ToString();
        UpdatedSoundsUI();
        UpdatedVibrateUI();
    }

    public void openShop()
    {
        shopPanel.SetActive(true);
        SoundManager.Instance.PlayButton();
    }

    public void closeShop()
    {
        shopPanel.SetActive(false);
        SoundManager.Instance.PlayButton();
    }

    public void openSettings()
    {
        settingsPanel.SetActive(true);
        SoundManager.Instance.PlayButton();
    }

    public void closeSettings()
    {
  
        settingsPanel.SetActive(false);
        SoundManager.Instance.PlayButton();
    }

    public void SoundsOnOff()
    {
        SoundManager.Instance.PlayButton();
        if(GameManager.Instance.SoundSettings)
        {
           GameManager.Instance.SoundSettings = false;
        }
        else
        {
           GameManager.Instance.SoundSettings = true;
        }
    }

    public void VibrateOnOff()
    {
        SoundManager.Instance.PlayButton();
        if(GameManager.Instance.VibrationSettings)
        {
            GameManager.Instance.VibrationSettings = false;
        }
        else
        {
            GameManager.Instance.VibrationSettings = true;
        }
    }

    private void UpdatedSoundsUI()
    {
        if(GameManager.Instance.SoundSettings)
        {
            soundsOn.SetActive(true);
            soundsOff.SetActive(false);
        }
        else
        {
            soundsOn.SetActive(false);
            soundsOff.SetActive(true);
        }
    }

    private void UpdatedVibrateUI()
    {
        if(GameManager.Instance.VibrationSettings)
        {
            vibrateOn.SetActive(true);
            vibrateOff.SetActive(false);
        }
        else
        {
            vibrateOn.SetActive(false);
            vibrateOff.SetActive(true);
        }
    }
}

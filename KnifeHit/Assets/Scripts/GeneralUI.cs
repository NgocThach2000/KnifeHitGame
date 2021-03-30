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

    private void Start()
    {
        //closeSettings();
    }

    private void Update()
    {
        totalApple.text = GameManager.Instance.TotalApple.ToString();
    }

    public void openShop()
    {
        shopPanel.SetActive(true);
    }

    public void closeShop()
    {
        shopPanel.SetActive(false);
    }

    public void openSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void closeSettings()
    {
        settingsPanel.SetActive(false);
    }

    public void SoundsOnOff()
    {
        if(soundsOn.activeSelf)
        {
            soundsOn.SetActive(false);
            soundsOff.SetActive(true);
        }
        else
        {
            soundsOn.SetActive(true);
            soundsOff.SetActive(false);
        }
    }

    public void VibrateOnOff()
    {
        if(vibrateOn.activeSelf)
        {
            vibrateOn.SetActive(false);
            vibrateOff.SetActive(true);
        }
        else
        {
            vibrateOn.SetActive(true);
            vibrateOff.SetActive(false);
        }
    }

}

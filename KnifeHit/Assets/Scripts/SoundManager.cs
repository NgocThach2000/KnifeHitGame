using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices.WindowsRuntime;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Sounds Clip")]
    [SerializeField] private AudioClip gameOverClip;
    [SerializeField] private AudioClip victoryClip;
    [SerializeField] private AudioClip buttonClip;
    [SerializeField] private AudioClip appleHitClip;
    [SerializeField] private AudioClip appleRewardClip;
    [SerializeField] private AudioClip wheelBreakClip;
    [SerializeField] private AudioClip wheelClip;
    [SerializeField] private AudioClip knifeClip;
    [SerializeField] private AudioClip fireKnifeClip;
    [SerializeField] private AudioClip unlockedClip;
    [SerializeField] private AudioClip fightStartClip;
    [SerializeField] private AudioClip fightEndClip;
    [SerializeField] private AudioClip homeClip;


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
        DontDestroyOnLoad(gameObject);
    }

    private void PlaySound(AudioClip clip, float vol = 1)
    {
        if(GameManager.Instance.SoundSettings)
        {
            if(Camera.main != null && GameManager.Instance.SoundSettings & clip != null)
            {
                AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, vol);
            }
        }
    }

    public void PlayButton()
    {
        PlaySound(buttonClip);
    }

    public void PlayAppleHit()
    {
        PlaySound(appleHitClip);
    }

    public void PlayWheelHit()
    {
        PlaySound(wheelClip);
    }

    public void PlayKnifeHit()
    {
        PlaySound(knifeClip);
    }

    public void PlayFireKnife()
    {
        PlaySound(fireKnifeClip);
    }

    public void PlayUnlock()
    {
        PlaySound(unlockedClip);
    }

    public void PlayBossStartFight()
    {
        PlaySound(fightStartClip);
    }

    public void PlayBossEndFight()
    {
        PlaySound(fightEndClip);
    }
    
    public void PlayAppleReward()
    {
        PlaySound(appleRewardClip);
    }

    public void PlayGameOver()
    {
        PlaySound(gameOverClip);
    }

     public void PlayVictory()
    {
        PlaySound(victoryClip);
    }

    public void PlayWheelBreak()
    {
        PlaySound(wheelBreakClip);
    }

     public void PlayHomeClip()
    {
        PlaySound(homeClip);
    }

    public void Vibrate()
    {
        if(GameManager.Instance.VibrationSettings)
        {
            Handheld.Vibrate();
        }
    }
}

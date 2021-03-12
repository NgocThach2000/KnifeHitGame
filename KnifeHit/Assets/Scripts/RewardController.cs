using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices.WindowsRuntime;
using Random = System.Random;


public class RewardController : MonoBehaviour
{
    public static RewardController Instance; 

    [SerializeField] private int hoursToReward;
    [SerializeField] private int minutesToReward;
    [SerializeField] private int secondsToReward = 10;
    
    private int minReward = 20;
    private int maxReward = 60;
    private const string NEXT_REWARD = "RewardTime";

    public DateTime NextRewardTime => GetNextRewardTime();

    private void Awake()
    {
        if(Instance)  //simpleton
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public bool CanReward()
    {
        return NextRewardTime <= DateTime.Now;
    }

    public int GetRandomReward()
    {
        return UnityEngine.Random.Range(minReward, maxReward);
    }
    
    public void ResetRewardTime()
    {
        DateTime nextReward = DateTime.Now.Add(new TimeSpan(hoursToReward, minutesToReward, secondsToReward));
        SaveNextRewardTime(nextReward);
    }

    private void SaveNextRewardTime(DateTime time)
    {
        PlayerPrefs.SetString(NEXT_REWARD, time.ToBinary().ToString());
        PlayerPrefs.Save();
    }

    private DateTime GetNextRewardTime()
    {
        string nextReward = PlayerPrefs.GetString(key: NEXT_REWARD, defaultValue: string.Empty);
        if(!string.IsNullOrEmpty(nextReward))
        {
            return DateTime.FromBinary(Convert.ToInt64(nextReward));
        }
        else
        {
            return DateTime.Now;
        }
    }
}

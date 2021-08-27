using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices.WindowsRuntime;
using Random = System.Random;
using System.Security;

public class RewardUI : MonoBehaviour
{
    [SerializeField] private GameObject rewardPannel;
    [SerializeField] private ParticleSystem applePs;
    [SerializeField] private Text rewardText;

    private void Update()
    {
        if(RewardController.Instance.CanReward())
        {
            rewardText.text = "Get Reward!";
        }
        else
        {
            TimeSpan timeToReward;
            timeToReward = RewardController.Instance.TimeToReward;
            rewardText.text = string.Format("{0:00}:{1:00}:{2:00}", timeToReward.Hours, timeToReward.Minutes, timeToReward.Seconds);
        }
    }  

    public void RewardPlayer()
    {
        if(RewardController.Instance.CanReward())
        {
            int amount = RewardController.Instance.GetRandomReward();
            StartCoroutine(RewardCR());
            RewardController.Instance.ResetRewardTime();

            SoundManager.Instance.PlayAppleReward();
            GameManager.Instance.TotalApple += amount;
        }
    }
    
    private IEnumerator RewardCR()
    {
        rewardPannel.SetActive(true);
        yield return new WaitForSeconds(1f);
        Instantiate(applePs);
        //Sounds Manager;
        yield return new WaitForSeconds(3f);
        rewardPannel.SetActive(false);
    }
}

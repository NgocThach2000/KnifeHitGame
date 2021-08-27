using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security;

public class MenuManager : MonoBehaviour
{
     // private string gameURL = "";
    // private string appID = "";
    // public void Rate()
    // {
    //    Application.OpenURL(gameURL + appID);
    //    ...Sound
    // }
    [SerializeField] private Image selectedKnife;
    public Image SelectedKnife => selectedKnife;

    public void Play()
    {
        SceneManager.LoadScene("Scenes/Game");
        SoundManager.Instance.PlayButton();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices.WindowsRuntime;

public class MainManager : MonoBehaviour
{
    // private string gameURL = "";
    // private string appID = "";
    // public void Rate()
    // {
    //    Application.OpenURL(gameURL + appID);
    // }

    public void Play()
    {
        SceneManager.LoadScene("Scenes/Game");
    }

    
}

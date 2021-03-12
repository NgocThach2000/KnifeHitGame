using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    private const string SELECTED_KNIFE = "Knife";
    private const string HIGHSCORE = "Highscore";
    private const string TOTAL_APPLE = "TotalApple";
    private const string SOUND_SETTINGS = "SoundSettings";
    private const string VIBRATION_SETTINGS = "VibrationSettings";

    public bool IsGameOver { get; set; }
    public int Stage {get; set;}
    public int Score {get; set;}
    public Knife SelectedKnifePrefab {get; set;}

    public float ScreenHeight => Camera.main.orthographicSize * 2;
    public float ScreenWidth => ScreenHeight / Screen.height * Screen.width;

    public int SelectedKnife
    {
        get => PlayerPrefs.GetInt(key: SELECTED_KNIFE, defaultValue: 0);
        set => PlayerPrefs.SetInt(SELECTED_KNIFE, value);
    }

    public int HighScore
    {
        get => PlayerPrefs.GetInt(key: HIGHSCORE, defaultValue: 0);
        set => PlayerPrefs.SetInt(HIGHSCORE, value);
    }

    public int TotalApple
    {
        get => PlayerPrefs.GetInt(key: TOTAL_APPLE, defaultValue: 0);
        set => PlayerPrefs.SetInt(TOTAL_APPLE, value);
    }

    public bool SoundSettings
    {
        get => PlayerPrefs.GetInt(key: SOUND_SETTINGS, defaultValue: 1) == 1;
        set => PlayerPrefs.SetInt(SOUND_SETTINGS, value ? 1 : 0);
    }

    public bool VibrationSettings
    {
        get => PlayerPrefs.GetInt(key: VIBRATION_SETTINGS, defaultValue: 1) == 1;
        set => PlayerPrefs.SetInt(VIBRATION_SETTINGS, value ? 1 : 0);
    }

    private void Awake()
    {
        if(Instance) //simpleton
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    private void Start()
    {
        
    }

    private void Update()
    {
        
    }
}

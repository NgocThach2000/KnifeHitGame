using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    
    public List<Wheel> wheels;
    public List<Boss> bosses;

    [SerializeField] private GameObject knifePrefab;

    [Header (header: "Wheel Settings")] 
    [SerializeField] private Transform wheelSpawnPosition;
    [Range(0,1)] [SerializeField] private float WheelScale;

    [Header (header: "Knife Settings")]
    [SerializeField] private Transform knifeSpawnPosition; 
    [Range(0,1)] [SerializeField] private float knifeScale;

    private string bossName;
    private Wheel currentWheel;
    private Knife currentKnife;

    public int TotalSpawnKnife {get; set;}

    private void Awake()
    {
        if(Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        InitializedGame();
    }

    private void Update()
    {
        if(currentKnife == null)
        {
            return ;
        }
        if(Input.GetMouseButtonDown(0) && !currentKnife.IsReleased)
        {
            knifeCounter.Instance.KnifeHit(TotalSpawnKnife);
            currentKnife.KnifeFire();
            StartCoroutine(routine: GenerateKnife());
        }
        
    }

    private void InitializedGame()
    {
        GameManager.Instance.IsGameOver = false;
        GameManager.Instance.Score = 0;
        GameManager.Instance.Stage = 1;

        SetupGame();
    }
    private void SetupGame()
    {
        SpawnWheel();
        knifeCounter.Instance.SetupKnife(currentWheel.AvailableKnifes);

        TotalSpawnKnife = 0;
        StartCoroutine( routine: GenerateKnife());
    }
    private void SpawnWheel()
    {
        GameObject tmpWheel = new GameObject();
        if(GameManager.Instance.Stage % 5 == 0)
        {
            //create a boss wheel
            Boss newBoss = bosses[Random.Range(0, bosses.Count)];
            tmpWheel = Instantiate(newBoss.bossPrefab, wheelSpawnPosition.position, Quaternion.identity, wheelSpawnPosition).gameObject;
        }
        else
        {
            tmpWheel = Instantiate(wheels[GameManager.Instance.Stage - 1], wheelSpawnPosition.position, Quaternion.identity, wheelSpawnPosition).gameObject;
        }
        float wheelScaleInScreen = GameManager.Instance.ScreenWidth * WheelScale / tmpWheel.GetComponent<SpriteRenderer>().bounds.size.x;
        tmpWheel.transform.localScale = Vector3.one * wheelScaleInScreen;
        currentWheel = tmpWheel.GetComponent<Wheel>();
    }

    private IEnumerator GenerateKnife()
    {
        yield return new WaitUntil( predicate: (()=> knifeSpawnPosition.childCount == 0));
        if(currentWheel.AvailableKnifes > TotalSpawnKnife && !GameManager.Instance.IsGameOver)
        {
            TotalSpawnKnife++;
            GameObject tmpKnife = new GameObject();
            if(GameManager.Instance.SelectedKnifePrefab == null)
            {
                tmpKnife = Instantiate(knifePrefab, knifeSpawnPosition.position, Quaternion.identity, knifeSpawnPosition).gameObject;
            }
            else
            {
                tmpKnife = Instantiate(GameManager.Instance.SelectedKnifePrefab, knifeSpawnPosition.position, Quaternion.identity, knifeSpawnPosition).gameObject;
            }
            float knifeScaleInScreen = GameManager.Instance.ScreenWidth * knifeScale / tmpKnife.GetComponent<SpriteRenderer>().bounds.size.y;
            tmpKnife.transform.localScale = Vector3.one * knifeScaleInScreen;
            currentWheel = tmpKnife.GetComponent<Wheel>();
        }
    }
    public void NextLevel()
    {
        if(currentWheel != null)
        {
            currentWheel.DestroyKnife();
        }
        if(GameManager.Instance.Stage % 5 == 0)
        {
            GameManager.Instance.Stage++;
            //UIManager need boss fight
        }
        else
        {
            GameManager.Instance.Stage++;
            if(GameManager.Instance.Stage % 5 == 0)
            {
                //UIManager display boss start fight
            }
            else
            {
                Invoke(nameof(SetupGame), time: 0.3f);
            }
        }
    }
}

public class Boss
{
    public GameObject bossPrefab;
    public string bossName;
}

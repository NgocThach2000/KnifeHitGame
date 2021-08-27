using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Wheel : MonoBehaviour
{
    [SerializeField] private int availableKnifes;
    [SerializeField] private Sprite firstWheel;
    [SerializeField] private Sprite secondWheel;
    [SerializeField] private Sprite thirdWheel;
    [SerializeField] private Sprite fourWheel;
    [SerializeField] private bool isBoss;

    [Header( header: "Prefabs")] 
    [SerializeField] private GameObject applePrefab;
    [SerializeField] private GameObject knifePrefab;
    [Header( header: "Settings")] 
    [SerializeField] private float duration;
    [SerializeField] private float speed;
    [SerializeField] private float roundStartTime;

    public List<Level> levels;

    [HideInInspector]
    public List<Knife> Knifes;

    private int levelIndex;
    
    public int AvailableKnifes => availableKnifes;

    private void Start()
    {
        if(!isBoss)
        {
            //Do something
            if(GameManager.Instance.Stage < 10)
            {
                GetComponent<SpriteRenderer>().sprite = firstWheel;
            }
            else if (GameManager.Instance.Stage > 10 && GameManager.Instance.Stage < 20)
            {
                GetComponent<SpriteRenderer>().sprite = secondWheel;
            }
            else if(GameManager.Instance.Stage > 20 && GameManager.Instance.Stage < 30)
            {
                GetComponent<SpriteRenderer>().sprite = thirdWheel;
            }
            else if(GameManager.Instance.Stage > 30)
            {
                GetComponent<SpriteRenderer>().sprite = fourWheel;
            }
        }
        RotateWheel();
        levelIndex = UnityEngine.Random.Range(0, levels.Count);
        if(levels[levelIndex].appleChance > UnityEngine.Random.value)
        {
            SpawnApple();
        }
        SpawnKnife();
    }

    private void RotateWheel()
    {   
        //transform.Rotate(0f,0f, speed * Time.deltaTime);
        float t = (Time.time - roundStartTime) / duration;
        t = 1 - t;
        float curRotationSpeed = speed * t;
        transform.Rotate(new Vector3(0, 0, curRotationSpeed) * Time.deltaTime);
        if (t < 0.05f)
        {
            if(!isBoss)
            {
                roundStartTime = Time.time;
                float roundPower = Random.Range(0,1f);
                speed = -150 - 150*roundPower;
                duration = 5 + 5 * roundPower;
            }
            else
            {
                roundStartTime = Time.time;
                float roundPower = Random.Range(0,1f);
                speed = -370 - 50*roundPower;
                duration = 3 + 5 * roundPower;
            }
        }    
    }

    private void Update()
    {
        RotateWheel();
    }

    private void SpawnKnife()
    {
        foreach (float knifeAngle in levels[levelIndex].knifeAngleFromWheel)
        {
            GameObject knifeTmp = Instantiate(knifePrefab);
            knifeTmp.transform.SetParent(transform);
            SetRotationFromWheel(transform, knifeTmp.transform, knifeAngle, 0f, 180f);
            knifeTmp.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            knifeTmp.GetComponent<Rigidbody2D>().isKinematic = true;
        }
    }
    private void SpawnApple()
    {
        foreach (float appleAngle in levels[levelIndex].appleAngleFromWheel)
        {
            GameObject appleTmp = Instantiate(applePrefab);
            appleTmp.transform.SetParent(transform);
            SetRotationFromWheel(transform, appleTmp.transform, appleAngle, 0.35f, 0f);
            appleTmp.transform.localScale = new Vector3(0.15f,0.15f,0.1f);
        }
    }
    public void SetRotationFromWheel(Transform wheel, Transform objectToPlace, float angle, float spaceFromObject, float objectRotation)
    {
        Vector2 offSet = new Vector2(Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad)) * (wheel.GetComponent<CircleCollider2D>().radius + spaceFromObject); //Change Z
        objectToPlace.localPosition = (Vector2) wheel.localPosition + offSet;
        objectToPlace.localRotation = Quaternion.Euler(0, 0, -angle + objectRotation);
    }
    public void KnifeHit(Knife knife)
    {
        knife.myRigidbody2D.isKinematic = true;
        knife.myRigidbody2D.velocity = Vector2.zero;
        knife.transform.SetParent(transform);
        knife.Hit = true;

        Knifes.Add(knife);
        if(Knifes.Count >= availableKnifes)
        {
            //Goto NextLevel
            LevelManager.Instance.NextLevel();
            
        }
        //Score++
        GameManager.Instance.Score++;
    }
    public void DestroyKnife()
    {
        foreach(var knife in Knifes)
        {
            Destroy(knife.gameObject);
        }
        Destroy(gameObject);
    }
}    
//level game
[Serializable] 
public class Level
{
    [Range(0, 1)] [SerializeField] public float appleChance;

    public List<float> appleAngleFromWheel = new List<float>();
    public List<float> knifeAngleFromWheel = new List<float>();
}



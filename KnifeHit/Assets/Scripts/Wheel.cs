﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    [SerializeField] private int availableKnifes;
    [SerializeField] private Sprite firstWheel;
    [SerializeField] private Sprite secondWheel;
    [SerializeField] private Sprite thirdWheel;
    [SerializeField] private bool isBoss;

    [Header( header: "Prefabs")] 
    [SerializeField] private GameObject applePrefab;
    [SerializeField] private GameObject knifePrefab;
    [Header( header: "Settings")] 
    [SerializeField] private float rotationTime;
    [SerializeField] private float rotationZ;

    public List<Level> levels;

    [HideInInspector]
    public List<Knife> Knifes;

    private int levelIndex;
    
    public int AvailableKnifes => availableKnifes;

    private void Start()
    {
        if(isBoss)
        {
            //Do something
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
        Mathf.LerpAngle(transform.localEulerAngles.z, transform.localEulerAngles.z + rotationZ, rotationTime);
    }
    private void SpawnKnife()
    {
        foreach (float knifeAngle in levels[levelIndex].knifeAngleFromWheel)
        {
            GameObject knifeTmp = Instantiate(knifePrefab);
            knifeTmp.transform.SetParent(transform);
            SetRotationFromWheel(transform, knifeTmp.transform, knifeAngle, 0.20f, 180f);
            knifeTmp.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
        }
    }
    private void SpawnApple()
    {
        foreach (float appleAngle in levels[levelIndex].appleAngleFromWheel)
        {
            GameObject appleTmp = Instantiate(applePrefab);
            appleTmp.transform.SetParent(transform);
            SetRotationFromWheel(transform, appleTmp.transform, appleAngle, 0.25f, 0f);
            appleTmp.transform.localScale = new Vector3(0.15f,0.15f,0.5f);
        }
    }
    public void SetRotationFromWheel(Transform wheel, Transform objectToPlace, float angle, float spaceFromObject, float objectRotation)
    {
        Vector2 offSet = new Vector2(Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad)) * (wheel.GetComponent<CircleCollider2D>().radius + spaceFromObject);
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

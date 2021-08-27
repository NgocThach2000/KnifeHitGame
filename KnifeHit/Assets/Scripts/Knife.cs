using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices.WindowsRuntime;
using Random = UnityEngine.Random;

public class Knife : MonoBehaviour
{
    [SerializeField] private float speed; 
    public Rigidbody2D myRigidbody2D;
    public bool IsReleased { get; set; }
    public bool Hit { get; set; }
    private void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();

    }

    public void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            FireKnife();
        }
    }
    public void FireKnife()
    {
        if(!IsReleased)
        {
            IsReleased = true;
            myRigidbody2D.AddForce(new Vector2(0f, speed), ForceMode2D.Impulse);
            SoundManager.Instance.PlayFireKnife();
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Wheel") && !Hit && !GameManager.Instance.IsGameOver && IsReleased)
        {
            other.gameObject.GetComponent<Wheel>().KnifeHit(this);
            GameManager.Instance.Score++;
            SoundManager.Instance.PlayWheelHit();
        }
        else if(other.gameObject.CompareTag("Knife") && !Hit && !GameManager.Instance.IsGameOver && other.gameObject.GetComponent<Knife>().IsReleased) 
        {
            Hit = true;
            transform.SetParent(other.transform);
            SoundManager.Instance.PlayKnifeHit();
            SoundManager.Instance.Vibrate();
            myRigidbody2D.velocity = Vector2.zero;
            //myRigidbody2D.isKinematic = true;
            myRigidbody2D.freezeRotation = false;
            myRigidbody2D.angularVelocity = Random.Range (20f, 50f) * 25f;
            myRigidbody2D.AddForce (new Vector2 (Random.Range (-5f, 5f), -30f), ForceMode2D.Impulse);
            GameManager.Instance.IsGameOver = true;
            Invoke(nameof(GameOver), 0.5f);
        }
    }
    private void GameOver()
    {
        //SoundManager.Instance.PlayGameOver();
        UIManager.Instance.GameOver();
        
    }

   
}

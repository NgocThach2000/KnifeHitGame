using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Knife : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float speed; //hide speed data => prevent crack 
    public Rigidbody2D myRigidbody2D;
    public bool IsReleased { get; set; }
    public bool Hit { get; set; }
    private void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();

    }
    //Update is called once per frame
    public void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            KnifeFire();
        }
    }
    public void KnifeFire()
    {
        if(!IsReleased)
        {
            IsReleased = true;
            myRigidbody2D.AddForce(new Vector2(0f, speed), ForceMode2D.Impulse);
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Wheel") && !Hit && !GameManager.Instance.IsGameOver && IsReleased)
        {
            other.gameObject.GetComponent<Wheel>().KnifeHit(this);
            GameManager.Instance.Score++;
        }
        else if(other.gameObject.CompareTag("Knife") && !Hit && IsReleased && !GameManager.Instance.IsGameOver && other.gameObject.GetComponent<Knife>().IsReleased) 
        {
            Hit = true;
            transform.SetParent(other.transform);
            myRigidbody2D.velocity = Vector2.zero;
            myRigidbody2D.isKinematic = true;

            GameManager.Instance.IsGameOver = true;
            Invoke(nameof(GameOver), 0.5f);
            //GameOver
            //SoundManager = sound
            //Restart level
        }
    }

    private void GameOver()
    {
        UIManager.Instance.GameOver();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime;

public class Apple : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private ParticleSystem appleParticle;
    private BoxCollider2D myCollider2D;
    private SpriteRenderer iot;
    private void Start()
    {
        myCollider2D = GetComponent<BoxCollider2D>();
        iot = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Knife"))
        {
            myCollider2D.enabled = false;
            iot.enabled = false;
            transform.parent = null;
            SoundManager.Instance.PlayAppleHit();

            GameManager.Instance.Score++;
            GameManager.Instance.TotalApple++;
            appleParticle.Play();
            Destroy(gameObject, t: 2f);
        }
    }
}





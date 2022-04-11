using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float speed;
    public Transform dir;
    public Stats playerStats;
    public GameObject conjurer;

    private void Awake()
    {
        playerStats = GameObject.Find("Player1").GetComponent<Stats>();
    }

    void Start()
    {
        Destroy(gameObject,2.5f);
        speed = 8f;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position += (transform.forward * speed * Time.deltaTime);
        transform.forward = dir.forward;
        transform.Translate(transform.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            if (other.gameObject.GetComponent<Stats>() != null)
            {
                var stats = other.gameObject.GetComponent<Stats>();
                if (stats.hp > (playerStats.mgk - stats.magicDef))
                {
                    stats.hp -= playerStats.mgk - stats.magicDef;
                    Debug.Log(stats.hp);
                }
                else
                {
                    stats.hp = 0f;
                    Debug.Log(stats.hp);
                    Destroy(other.gameObject);
                }

                Destroy(gameObject);
            }
        }
    }
    
}

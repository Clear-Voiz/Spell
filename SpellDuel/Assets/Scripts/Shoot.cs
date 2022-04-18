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
    public Spell spell;

    private void Awake()
    {
        playerStats = GameObject.Find("Player1").GetComponent<Stats>();
    }

    void Start()
    {
        Destroy(gameObject,spell.lifespan);
    }

    // Update is called once per frame
    void Update()
    {
        if (spell != null && dir != null)
        {//transform.position += (transform.forward * speed * Time.deltaTime);
            spell.Move();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        spell.Impact(other);
    }
    
}

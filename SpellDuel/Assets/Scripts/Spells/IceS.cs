using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class IceS : Spell
{
    private int amount;
    private float interval;
    private GameObject iceShard;
    private float elapsedTime;

    private void Awake()
    {
        iceShard = Resources.Load("IceShard") as GameObject;
    }

    private void Start()
    {
        interval = 0.2f;
        amount = 10;
        _conjure = FindObjectOfType<Conjure>();
    }

    private void Update()
    {
      Timer();
    }

    private void Timer()
    {
        if (elapsedTime < interval)
        {
            elapsedTime += Time.deltaTime;
        }
        else
        {
            Freezer();
        }

        if (amount <= 0)
        {
            Destroy(this);
        }
    }

    
    private void Freezer()
    { 
        Vector3 place = new Vector3(Random.Range(_conjure.ori.position.x - 2f, _conjure.ori.position.x + 2f),
            Random.Range(_conjure.ori.position.y + 1f, _conjure.ori.position.y + 3f), _conjure.ori.position.z*1.5f);
            
        Instantiate(iceShard, place, Quaternion.identity);
        elapsedTime = 0f;
        amount -= 1;
    }
}

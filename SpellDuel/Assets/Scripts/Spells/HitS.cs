using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitS : Spell,IShootable
{
    private void Start()
    {
        speed = 18f;
        lifespan = 3f;
        PM = 0.5f;
        Element = Elements.NonElemental;
        Destroy(gameObject,lifespan);
    }

    private void Update()
    {
        Shoot();
    }

    private void OnTriggerEnter(Collider other)
    {
        Damager(other);
    }

    public void Shoot()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}

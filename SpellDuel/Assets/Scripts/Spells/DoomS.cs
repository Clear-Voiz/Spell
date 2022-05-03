using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class DoomS : Spell,IShootable
{
    private void Awake()
    {
        PM = 2f;
        Element = Elements.NonElemental;
        _conjure = FindObjectOfType<Conjure>();
    }

    private void Start()
    {
        speed = 20f;
    }

    private void Update()
    {
        Shoot();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            var temp = new SDoom(other, PM, LP,_conjure);
            _conjure.effectsManager.ActiveEffects.Add(temp);
            Destroy(gameObject);
        }
    }

    public void Shoot()
    {
        transform.Translate(Vector3.forward*speed * Time.deltaTime);
    }
}

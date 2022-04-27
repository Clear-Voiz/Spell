using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class ShieldS : Spell
{
    private void Start()
    {
        Destroy(gameObject,5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Spell"))
        {
            Spell spell;
            if (other.TryGetComponent(out spell))
            {
                Debug.Log("it works");
                if (spell.Element == Elements.NonElemental)
                {
                    spell.speed = 0f;
                    //Destroy(other.gameObject);
                }
                else
                {
                    LP = Resources.Load("LocalPoints") as GameObject;
                    Instantiate(LP, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                }
            }
            else
            {
                Debug.Log("false");
            }
        }
    }
}

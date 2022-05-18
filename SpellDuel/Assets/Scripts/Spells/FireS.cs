﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireS : Spell,IControllable,IShootable
{
        public static string[] definition = {"Fire</color></b>: casts a flame controllable with your wand.","Firing</color></b>: Increased size and damage.","Fired</color></b>: Explodes when colliding or manually by left-pressing your wand."};
        

        private void Awake()
        {
                _conjure = FindObjectOfType<Conjure>();
        }

        private void Start()
        {
                //VFX = Resources.Load("PS_FireBall") as GameObject;
                ImpactVFX = null;
                lifespan = 3f;
                speed = 8f;
                PM = 0.6f; //Power Multiplier
                cost = 2f;
                Element = Elements.Fire;
                ActiveCol = "#ff0000ff";
                InactiveCol = "#800000ff";
                Destroy(gameObject,lifespan);
        }

        private void Update()
        { 
                Control();
                Shoot();
        }

        public void Control()
        { 
                //transform.forward = _conjure.ori.forward;
                var hitPoint = _conjure.aimAt.pointer;
                var direction = hitPoint.position - transform.position;
                Quaternion rot = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, rot, 1);
        }

        public void Shoot()
        {
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
                Damager(other);  
        }
}

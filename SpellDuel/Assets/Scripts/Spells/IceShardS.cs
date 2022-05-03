using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceShardS : Spell,IShootable
{
    private Vector3 dir;
    private Camera _mCam;
    private float rotSpeed;
    private Collider _col;
    private void Awake()
    {
        _mCam = Camera.main;
        _conjure = FindObjectOfType<Conjure>();
        _col = GetComponent<Collider>();

    }

    private void Start()
    {
        PM = 0.3f;
        speed = 20f;
        rotSpeed = 360f;
        var hitpoint = _conjure.aimAt.pointer.position;
        dir = hitpoint - transform.position;
        Quaternion rot = Quaternion.LookRotation(dir);
        transform.rotation = rot;
    }

    private void Update()
    {
        transform.Rotate(0f,0f, rotSpeed*Time.deltaTime,Space.Self);
        Shoot();
    }

    public void Shoot()
    {
        transform.Translate(Vector3.forward*speed*Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Damager(other);
            transform.SetParent(other.transform);
            SSlow slow = new SSlow(_conjure);
            _conjure.effectsManager.ActiveEffects.Add(slow);
            _col.enabled = false;
        }

        if (!other.CompareTag("Spell"))
        {
            speed = 0f;
            rotSpeed = 0f;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float speed;
    public Transform dir;
    void Start()
    {
        Destroy(gameObject,2.5f);
        speed = 8f;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position += (transform.forward * speed * Time.deltaTime);
        transform.Translate(dir.up * speed * Time.deltaTime);
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}

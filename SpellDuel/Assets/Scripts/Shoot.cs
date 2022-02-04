using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float speed;
    void Start()
    {
        Destroy(gameObject,2f);
        speed = 8f;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position += (transform.forward * speed * Time.deltaTime);
        transform.Translate(speed * Time.deltaTime,0f,0f);
        
    }
}

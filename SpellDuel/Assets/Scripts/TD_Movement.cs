using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TD_Movement : MonoBehaviour
{
    public float speed;
    public Transform player;
    void Start()
    {
        speed = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");
        var movimiento = new Vector3(horizontal, 0f, 0f);

        if (movimiento.magnitude >= 0.1f)
        {
            var angle = Mathf.Atan2(movimiento.x, movimiento.z) * Mathf.Rad2Deg; //radianes a grados
            player.position += (movimiento * speed * Time.deltaTime);

        }
    }
}

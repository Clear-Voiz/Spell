using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class AimAt : MonoBehaviour
{ //this is attached to the wand gameobject
    
    private Ray rayMouse;


    private void Update()
    {
        //Camera.current
        /*RaycastHit hit;
        var mousePos = Input.mousePosition;
        rayMouse = Camera.current.ScreenPointToRay(mousePos);*/
        var vectorMouse = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 100f);
        Vector3 mousePos =Camera.main.ScreenToWorldPoint(vectorMouse);
        Vector3 direction = mousePos - transform.position;
        Quaternion rot = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, 1);
    

        //if (Physics.Raycast(rayMouse.origin, rayMouse.direction, out hit)) ;
    }
}

using System;
using FishNet.Object;
using UnityEngine;
using UnityEngine.VFX;

public class AimAt : NetworkBehaviour
{ //this is attached to the wand gameobject
    
   // private Ray rayMouse;

   public Transform pointer;
   private Camera _mCam;
   private Vector3 Direction;
   public Quaternion rot;
   private VisualEffect _light;
   private const string colorVarName = "Color";
   private Color lColor = Color.red;
   private RaycastHit hit;

   private void Awake()
   {
       _light = pointer.GetComponent<VisualEffect>();
   }

   public Color LColor
   {
       get => lColor;
       set
       {
           if (value != lColor)
           {
               lColor = value;
               _light.SetVector4(colorVarName,value);
           }
       }
   }
   

   private void Start()
   {
       _mCam = Camera.main;

   }

   private void Update()
   {
       if (hit.collider==null) return;
       if(hit.collider.CompareTag("Controllable")) LColor = Color.blue;
       else
       {
           LColor = Color.yellow;
       }
   }

   private void FixedUpdate()
   {
       if (!IsOwner) return;
       Ray ray = _mCam.ScreenPointToRay(Input.mousePosition);
       Physics.Raycast(ray, out hit,50f,1<<12); // 
       pointer.position = hit.point;
       Direction = hit.point - transform.position;
       rot = Quaternion.LookRotation(Direction);
       transform.rotation = Quaternion.Slerp(transform.rotation, rot, 1);
       Debug.DrawLine(transform.position,hit.point);




       //if (Physics.Raycast(rayMouse.origin, rayMouse.direction, out hit)) ;
    }


   
}

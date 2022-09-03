using FishNet.Object;
using UnityEngine;

public class AimAt : NetworkBehaviour
{ //this is attached to the wand gameobject
    
   // private Ray rayMouse;

   public Transform pointer;
   private Camera _mCam;
   private Vector3 Direction;
   public Quaternion rot;

   private void Start()
   {
       _mCam = Camera.main;

   }
   
   private void Update()
   {
       if (!IsOwner) return;
       Ray ray = _mCam.ScreenPointToRay(Input.mousePosition);
       Physics.Raycast(ray, out RaycastHit hit,50f,1<<12); // 
       pointer.position = hit.point;
       Direction = hit.point - transform.position;
       rot = Quaternion.LookRotation(Direction);
       transform.rotation = Quaternion.Slerp(transform.rotation, rot, 1);
       
        //if (Physics.Raycast(rayMouse.origin, rayMouse.direction, out hit)) ;
    }
   
   
}

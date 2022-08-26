using FishNet.Object;
using UnityEngine;
public class ImpulseS: Spell
{
    private bool trigger;
    //private Vector3 dir;
    private Rigidbody rb;
    private Collider col;
    private void Awake()
    {
        PM = 1.2f; //Power Multiplier
        Element = Elements.NonElemental;
        cooldown = 3f;
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        if(!IsOwner) return;
        TakeOver();
    }


    private void Update()
    {
        if (!IsOwner) return;
        if (rb == null) return;
        
            if (trigger)
            {
                //_subject.transform.localRotation = _conjure.ori.rotation;
                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log("Yeeted and deleted");
                    trigger = false;
                    Vector3 dir = (_conjure.aimAt.pointer.position - rb.position);
                    dir = dir.normalized * _conjure.stats.str*6f;
                    rb.AddForce(dir,ForceMode.Impulse);
                    rb.useGravity = true;
                    Despawner();
                    //rb.AddForce(_subject.transform.forward*12f,ForceMode.Impulse);
                }
            }
            /*else
        {
            //Destroy(gameObject);
            //DESPAWN HERE
        }*/
    }

    [ServerRpc]
    private void TakeOver()
    {
        Debug.Log("Raycast thrown");
        Physics.Raycast(_conjure.ori.position, _conjure.ori.forward, out RaycastHit hit);
        if (!hit.collider.CompareTag("Controllable")) Despawner();
        //if (hit.collider.gameObject)
        if (hit.collider.TryGetComponent(out rb))
        {
            Debug.Log("Gotcha now!");
            PM = rb.mass; //POTENTIAL CAUSE OF NOT RECEIVING DAMAGE
            rb.useGravity = false;

        }
        trigger = true;
    }
    
}

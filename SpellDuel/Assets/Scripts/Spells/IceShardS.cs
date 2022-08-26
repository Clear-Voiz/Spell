using FishNet.Connection;
using FishNet.Object;
using UnityEngine;

public class IceShardS : Spell,IShootable
{
    private Vector3 dir;
    private float rotSpeed;
    private Collider _col;
    private Timers tim;


    

    public override void OnStartClient()
    {
        base.OnStartClient();
        //if (!IsOwner) return;
        _col = GetComponent<Collider>();
        
        PM = 0.3f;
        Element = Elements.Ice;
        speed = 20f;
        rotSpeed = 360f;
        
        var hitpoint = _conjure.aimAt.pointer.position;
        dir = hitpoint - transform.position;
        Quaternion rot = Quaternion.LookRotation(dir);
        transform.rotation = rot;
        lifespan = 3f;
        tim = new Timers(1);
        tim.alarm[0] = lifespan;

    }


    private void Update()
    {
        if(!IsOwner) return;
        transform.Rotate(0f,0f, rotSpeed*Time.deltaTime,Space.Self);
        Shoot();
        tim.alarm[0] = tim.Timer(lifespan, tim.alarm[0], Despawner);
    }

    public void Shoot()
    {
        transform.Translate(Vector3.forward*speed*Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!IsOwner) return;
        tim.alarm[0] = 0f;
        
        //StartCoroutine(DestroyAfter(3f));
        Clash(other);
        if (other.CompareTag("Player"))
        {
            //transform.SetParent(other.transform);
            if (other.TryGetComponent(out NetworkObject nob))
            {
                if (nob.IsOwner) return;
                PreAffect(nob.Owner);
            }
            
            //_conjure.effectsManager.ActiveEffects.Add(slow);
            _col.enabled = false;
        }

        if (!other.CompareTag("Spell"))
        {
            speed = 0f;
            rotSpeed = 0f;
        }
    }

    [ServerRpc]
    private void PreAffect(NetworkConnection conn)
    {
        Affect(conn);
    }
    
    
    [TargetRpc]
    private void Affect(NetworkConnection conn)
    {
        //Instantiate(_conjure.SH.)
        AlterSpell alterSpell = new SSlow();
        _conjure.effectsManager.AddDebuff(alterSpell);
    }
}

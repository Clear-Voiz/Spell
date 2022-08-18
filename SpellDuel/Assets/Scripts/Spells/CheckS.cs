using FishNet;
using FishNet.Object;
using UnityEngine;

public class CheckS : Spell
{
    private Rigidbody _rig;
    private Timers tim;
    private void Awake()
    {
        _rig = GetComponent<Rigidbody>();
        tim = new Timers(2);
        Element = Elements.NonElemental;
        _conjure = FindObjectOfType<Conjure>();
        PM = 0.5f;
        LP = _conjure.SH.LP;
        cooldown = 3f;

    }

    private void Update()
    {
        tim.alarm[0] = tim.Timer(0.7f, tim.alarm[0], Fall);
        if(!IsOwner) return;
        tim.alarm[1] = tim.Timer(4f, tim.alarm[1], Finish);
    }

    private void Fall()
    {
        _rig.useGravity = true;
    }

    [ServerRpc]
    private void Finish()
    {
        Despawner();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            _rig.useGravity = false;
            _rig.velocity = Vector3.zero;
        }
        
        if (other.CompareTag("Player"))
        {
            var stats = other.GetComponent<Stats>();
            if (stats.hp <= stats.maxHp.Value / 2)
            {
                InstanceFinder.ServerManager.Despawn(other.gameObject);
            }
            else
            {
                Clash(other);
            }
        }
    }
}

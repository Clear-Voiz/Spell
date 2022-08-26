using FishNet.Connection;
using FishNet.Object;
using UnityEngine;

public class DoomS : Spell,IShootable
{
    private void Awake()
    {
        PM = 2f;
        Element = Elements.NonElemental;
        cooldown = 5f;
        speed = 20f;
    }

    private void Update()
    {
        if(!IsOwner) return;
        Shoot();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) Despawn();
        if (!IsOwner) return;
        if (other.TryGetComponent(out NetworkObject nob))
        {
            if (nob.IsOwner) return;
            Judge(nob.Owner);
        }
    }

    public void Shoot()
    {
        transform.Translate(Vector3.forward*speed * Time.deltaTime);
    }

    [ServerRpc]
    protected void Judge(NetworkConnection conn)
    {
        
        SDoom judge = Instantiate(_conjure.SH.judge);
        if (_conjure.enemy.gameObject.TryGetComponent(out Conjure conjure))
        {
            judge._conjure = conjure;
            Spawn(judge.gameObject,conn);
            _conjure.effectsManager.AddDebuff(judge);
        }
        
        Despawn();
    }

}

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
            Telephone(nob.Owner);
            Despawner();
        }
    }

    public void Shoot()
    {
        transform.Translate(Vector3.forward*speed * Time.deltaTime);
    }

    [TargetRpc]
    private void Judge(NetworkConnection conn)
    {
        new SDoom(_conjure, PM);
    }
    
    [ServerRpc]
    protected void Telephone(NetworkConnection conn)
    {
        Judge(conn);
    }

}

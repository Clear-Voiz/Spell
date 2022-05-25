using System;
using FishNet.Object;
using UnityEngine;

public sealed class PawnWeapon : NetworkBehaviour
{
    private Pawn _pawn;
    private PawnInput _input;

    [SerializeField] private float damage;

    [SerializeField]
    private float shotDelay;

    private float timeUntilNextShot;

    [SerializeField] private Transform firePoint;
    
    public override void OnStartNetwork()
    {
        base.OnStartNetwork();

        _pawn = GetComponent<Pawn>();
        _input = GetComponent<PawnInput>();
    }

    private void Update()
    {
        if (!IsOwner) return;
        if (timeUntilNextShot <= 0f)
        {
            if (_input.fire)
            {
                ServerFire(firePoint.position,firePoint.forward);
                timeUntilNextShot = shotDelay;

            }
        }
        else
        {
            timeUntilNextShot -= Time.deltaTime;
        }
    }

    
    [ServerRpc]
    private void ServerFire(Vector3 FirePointPos, Vector3 firePointDirection)
    {
        /*if (Physics.Raycast(FirePointPos, firePointDirection, out RaycastHit hit) && hit.transform.TryGetComponent(out Pawn pawn))
        {
            pawn.ReceiveDamage(damage);
        }*/
    }
}

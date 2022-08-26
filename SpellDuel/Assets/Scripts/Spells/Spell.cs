using System;
using System.Collections;
using FishNet.Connection;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine;

[Flags]
public enum Elements{NonElemental = 0,Fire = 1,Ice = 2,Thunder = 4,Earth = 8,Water = 16,Wind = 32,Light = 64, Dark= 128}

public abstract class Spell: NetworkBehaviour
{
    //var definitions
    protected GameObject ImpactVFX;
    protected float lifespan;
    public float speed;
    protected float PM; //Power Multiplier
    protected float cost;
    public Elements Element;
    protected GameObject LP;
    public float cooldown;

    [SyncVar]
    public Conjure _conjure;
    
    
    protected void Clash(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            if (other.TryGetComponent(out NetworkObject nob))
            {
                if (nob.IsOwner) return;
                speed = 0f;
                DamageCalc();
            }
        }

        else
        {
            if (!other.CompareTag("Spell"))
            {
                speed = 0f;
                //Fade Away (not necessary, the spell should play its animation effect and THEN fade away. Meanwhile it just stays in place until the end of its lifespawn)
            }
        }
    }

    [ServerRpc]
    public void DamageCalc()
    {
        var stats = _conjure.enemy.GetComponent<Stats>();
        var tmpdmg = (Globs.mgk.Value - stats.mgkDef.Value) * PM * stats.RES[Element];
        Debug.Log("mgk: " + Globs.mgk.Value + " - def: " +stats.mgkDef.Value + " * PM: " + PM + " * " + "stats.RES: " + stats.RES[Element] + " = " + tmpdmg);
        int damage = Mathf.RoundToInt(tmpdmg);
        if (damage < 1)
        {
            damage = 1;
        }
                    
        stats.TakeDamage(Element,damage);
    }

    [ServerRpc]
    public void Despawner()
    {
        if (IsSpawned) Despawn();
    }
    
   
    protected IEnumerator DestroyAfter(float t)
    {
        yield return new WaitForSeconds(t);
        Despawner();
    }

    /*[ServerRpc]
    protected void Inflict(Stats stats, int damage, NetworkConnection conn)
    {
        if (stats.RES[Element] > 1f)
        {
            BonusPoints(conn);
        }
        
        if (stats.HP > damage)
        {
            stats.HP -= damage;
        }
        else
        {
            stats.HP = 0f;
            stats.SettleGame(conn);
            //Destroy(other.gameObject);
        }
    }*/

    [ObserversRpc]
    protected void BonusPoints(NetworkConnection conn)
    {
       
            LP = _conjure.SH.LP;
            GameObject lp = Instantiate(LP, transform.position, Quaternion.identity);
            if (lp.TryGetComponent(out LocalPoints slp))
            {
                slp.conn = conn;
            }
    }

    /*[TargetRpc]
    protected void Affect(NetworkConnection conn, AlterSpell alterSpell)
    {
        _conjure.effectsManager.AddEffect(alterSpell);
    }*/

}

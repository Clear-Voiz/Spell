using System;
using System.Collections.Generic;
using FishNet.Connection;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine;

public enum GameConditions
{
    None =0,Victorious=1,Defeated=2
}

public class Stats : NetworkBehaviour
{
    public SO_Ficha pjFicha;
    public float scaleFact = 1.25f;
    public string Name;
    [HideInInspector] public int lvl;
    [HideInInspector] public float exp;
    [HideInInspector] public float maxExp;
    [HideInInspector] public float str;
    [HideInInspector] public float def;
    [HideInInspector] [SyncVar] public float cSpd; //client version. Change on the server
    private HUDs Huds;
    private Conjure _conjure;


    public Dictionary<Elements, float> RES = new Dictionary<Elements, float>(8);
    
    public CharacterStat mgk;
    public CharacterStat xpGain; //this will store the amount of xp multiplier. Add numbers between 0 and 1 to modifier list. Although percentage treat as flat
    public CharacterStat mgkDef;
    public CharacterStat maxHp;
    [HideInInspector] [SyncVar(SendRate = 0)] public float hp;
    public CharacterStat maxMp;
    [HideInInspector] public float mp;
    public CharacterStat spd; //this one is for the server
    
    [HideInInspector] public GameConditions currentState = GameConditions.None;
    
    public static event Action<Stats> OnEnd; 

    public float HP
    {
        get => hp;

        set
        {
            /*if (value <= 0f)
            {
                if (!IsOwner) return;
                OnDefeat?.Invoke(transform);
            }*/
            hp = value;
        }
    }
    
    

    /*public static CharacterStat fireRes;
    public static CharacterStat thunderRes = new CharacterStat(1f);
    public static CharacterStat iceRes = new CharacterStat(1f);
    public static CharacterStat lightRes = new CharacterStat(1f);
    public static CharacterStat darkRes = new CharacterStat(1f);*/
    
    private void Awake()
    {
        SetupPlayer();
        _conjure = GetComponent<Conjure>();

        //new CharacterStat(6f);
        //if (!IsOwner) return;

    }

    private void OnEnable()
    {
        HUDs.OnExisting += setHud;
    }
    private void OnDisable()
    {
        HUDs.OnExisting -= setHud;
    }

    public void SetupPlayer()
    {
        Name = pjFicha.Name;
        lvl = pjFicha.lvl;
        exp = pjFicha.exp;
        maxExp = pjFicha.maxExp;
        maxHp = new CharacterStat(pjFicha.maxHp);
        hp = maxHp.Value;
        mgk = new CharacterStat(pjFicha.mgk);
        mgkDef = new CharacterStat(pjFicha.mgkDef);
        xpGain = new CharacterStat(1f);
        maxMp = new CharacterStat(pjFicha.maxMp);
        mp = maxMp.Value;
        spd = pjFicha.spd;
        cSpd = spd.Value;//

        str = pjFicha.str;
        def = pjFicha.def;
        
        /*RES[Elements.NonElemental] = pjFicha.RES[Elements.NonElemental];
        RES[Elements.Fire] =  pjFicha.RES[Elements.Fire];
        RES[Elements.Ice] =  pjFicha.RES[Elements.Ice];
        RES[Elements.Thunder] =  pjFicha.RES[Elements.Thunder];
        RES[Elements.Earth] =  pjFicha.RES[Elements.Earth];
        RES[Elements.Water] =  pjFicha.RES[Elements.Water];
        RES[Elements.Wind] =  pjFicha.RES[Elements.Wind];
        RES[Elements.Light] =  pjFicha.RES[Elements.Light];
        RES[Elements.Dark] =  pjFicha.RES[Elements.Dark];*/
        RES.Add(Elements.Fire,1.2f);
        RES.Add(Elements.Thunder,1.2f);
        RES.Add(Elements.Ice,1f);
        RES.Add(Elements.Water,1f);
        RES.Add(Elements.Earth,1f);
        RES.Add(Elements.Wind, 1f);
        RES.Add(Elements.Light,1f);
        RES.Add(Elements.Dark,1f);
        RES.Add(Elements.NonElemental,0.8f);
    }

    /*public void ResetPlayerStats()
    {
        Name = pjFicha.Name;
        lvl = pjFicha.lvl;
        exp = pjFicha.exp;
        maxExp = pjFicha.maxExp;
        maxHp = new CharacterStat(pjFicha.maxHp);
        hp = maxHp.Value;
        mgk = new CharacterStat(pjFicha.mgk);
        mgkDef = new CharacterStat(pjFicha.mgkDef);
        xpGain = new CharacterStat(1f);
        maxMt = new CharacterStat(pjFicha.maxMp);
        mt = 0f;
        spd = new CharacterStat(pjFicha.speed);
        cSpd = spd.Value;

        str = pjFicha.str;
        def = pjFicha.def;
    }*/

    [ServerRpc(RequireOwnership = false)]
    private void setHud(HUDs huds)
    {
        Huds = huds;
        huds.SetHUDStats();
    }
    
    [ObserversRpc]
    public void SettleGame(NetworkConnection conn)
    {
        if (conn == LocalConnection)
        {
            currentState = GameConditions.Victorious;
            Player.Instance.VictoryCounter += 1;
            //Debug.Log(Huds.contendantStats);
            OnEnd?.Invoke(this);
            Debug.Log("onEnd invoked victory");
        }
        else
        {
            currentState = GameConditions.Defeated;
            Player.Instance.DefeatCounter += 1;
            OnEnd?.Invoke(this);
            Debug.Log("onEnd invoked Defeat");
        }
    }

    [Server]
    public void TakeDamage(Elements Element,int damage)
    {
        Debug.Log("Last barrier surpassed");
        if (RES[Element] > 1f)
        {
            BonusPoints(LocalConnection);
        }
        
        if (HP > damage)
        {
            Debug.Log("Can you surprise me even more?: "+damage);
            HP -= damage;
        }
        else
        {
            HP = 0f;
            SettleGame(LocalConnection);
            //Destroy(other.gameObject);
        }
    }
    
    [ObserversRpc]
    protected void BonusPoints(NetworkConnection conn)
    {
       
        GameObject LP = _conjure.SH.LP;
        GameObject lp = Instantiate(LP, transform.position, Quaternion.identity);
        if (lp.TryGetComponent(out LocalPoints slp))
        {
            slp.conn = conn;
        }
    }
}

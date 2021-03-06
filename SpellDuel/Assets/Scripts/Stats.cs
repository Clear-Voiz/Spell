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
    public int lvl;
    public float exp;
    public float maxExp;
    public float str;
    public float def;
    private HUDs Huds;


    public Dictionary<Elements, float> RES = new Dictionary<Elements, float>(8);
    
    public CharacterStat mgk;
    public CharacterStat xpGain; //this will store the amount of xp multiplier. Add numbers between 0 and 1 to modifier list. Although percentage treat as flat
    public CharacterStat mgkDef;
    public CharacterStat maxHp;
    [SyncVar(SendRate = 0)] public float hp;
    public CharacterStat maxMt;
    public float mt;
    public CharacterStat spd;
    
    public GameConditions currentState = GameConditions.None;
    public int VictoryCounter;
    public int DefeatCounter;
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
        maxMt = new CharacterStat(pjFicha.maxMp);
        mt = 0f;
        spd = new CharacterStat(pjFicha.speed);

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

    public void ResetPlayerStats()
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

        str = pjFicha.str;
        def = pjFicha.def;
    }

    private void setHud(HUDs huds)
    {
        if (!IsOwner) return;
        Huds = huds;
        Huds.SetHUDStats();
    }
    
    [ObserversRpc]
    public void SettleGame(NetworkConnection conn)
    {
        if (conn == GameManager.Instance.LocalConnection)
        {
            currentState = GameConditions.Victorious;
            VictoryCounter += 1;
            //Debug.Log(Huds.contendantStats);
            OnEnd?.Invoke(this);
            Debug.Log("onEnd invoked victory");
        }
        else
        {
            currentState = GameConditions.Defeated;
            DefeatCounter += 1;
            OnEnd?.Invoke(this);
            Debug.Log("onEnd invoked Defeat");
        }
    }
}

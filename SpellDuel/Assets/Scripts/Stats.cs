using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public SO_Ficha pjFicha;
    public float scaleFact = 1.25f;
    public string Name;
    public int lvl;
    public float exp;
    public float maxExp;
    public float hp;
    public float maxHp;
    public float mp;
    public float maxMp;
    public float str;
    public float mgk;
    public float def;
    public float magicDef;
    public float dex;
    public float speed;
    public float eva;
    public float aim;
    public float luck;
    public float dmg;
    public float stamina;
    public Dictionary<Elements, float> RES = new Dictionary<Elements, float>(8);
        
    private void Awake()
    {
        SetupPlayer();
    }

    
    private void SetupPlayer()
    {
        Name = pjFicha.Name;
        lvl = pjFicha.lvl;
        exp = pjFicha.exp;
        maxExp = pjFicha.maxExp;
        hp = pjFicha.hp;
        maxHp = pjFicha.maxHp;
        mp = pjFicha.mp;
        maxMp = pjFicha.maxMp;
        str = pjFicha.str;
        mgk = pjFicha.mgk;
        def = pjFicha.def;
        magicDef = pjFicha.mgkDef;
        dex = pjFicha.dex;
        speed = pjFicha.speed;
        eva = pjFicha.eva;
        aim = pjFicha.aim;
        luck = pjFicha.luck;
        dmg = pjFicha.dmg;
        stamina = pjFicha.stamina;
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
}

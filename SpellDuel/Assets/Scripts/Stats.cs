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
    }
}

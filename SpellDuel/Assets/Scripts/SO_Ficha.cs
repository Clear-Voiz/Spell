using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ficha_",menuName = "ScriptableObjects/Create new ficha...")]
public class SO_Ficha : ScriptableObject
{
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
    public float mgkDef;
    public float dex;
    public float luck;
    public float eva;
    public float aim;
    public float speed;
    public float stamina;
    public float dmg;
}

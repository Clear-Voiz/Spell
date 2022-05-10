using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/SpellInfo", fileName = "Info_")]
public class SO_SpellSpreadSheet : ScriptableObject
{
    public string id;
    public Sprite icon;
    public string description;
    public string titleActiveCol;
    public string titleInactiveCol;
    public string bodyActiveCol;
    public string bodyInactiveCol;
    public float manaCost;
    public float duration;
    public float powerMultiplier;
    public AnimationCurve scaleCurve;
    public ScriptableObject evolutionTo;
    public ScriptableObject regressionTo;
    public int requiredPoints; //required points to evolve the spell
}

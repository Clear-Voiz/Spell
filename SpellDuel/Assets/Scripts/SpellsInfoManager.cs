using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public class SpellsInfoManager : MonoBehaviour
{
    /*public Dictionary<string, SO_SpellInfo> SData;
    private string path = "file:/UnityProjects/Spell/SpellDuel/Assets/Scripts/ScriptableObjects/InfoSpells";
    public SO_SpellInfo test;
    private List<SO_SpellInfo> listi;
    private SO_SpellInfo[] testers;
    private void Awake()
    {
        SData = new Dictionary<string, SO_SpellInfo>();*/
        /*SData.Add("Fire",5);
        SData.Add("Ice",10);
        SData.Add("Thunder",35);
        SData.Add("Doom",8);
        SData.Add("Whisper",20);
        SData.Add("Earth",10);
        SData.Add("Water",15);
        SData.Add("Darkness",25);
        SData.Add("Impulse",5);
        SData.Add("Presto",5);
        SData.Add("Check",25);
        SData.Add("Shield",10);
        SData.Add("Vanish",15);
        SData.Add("Paralysis",12);*/

        //SData = AssetDatabase.LoadAllAssetsAtPath(path).ToDictionary(key => key.name, value => value );
        
        //Debug.Log(testers.Length);
        /*if (testers.Length > 0) SData = testers.ToDictionary(x => x.name, x => x);*/
        


    //}

    /*private void Start()
    {
        StartCoroutine(Loader());
       
    }*/

    /*private IEnumerator Loader()
    {
        testers = AssetDatabase.LoadAllAssetsAtPath(path).Where(x => x is SO_SpellInfo) as SO_SpellInfo[];
        while (testers == null)
        {
            testers = AssetDatabase.LoadAllAssetsAtPath(path).Where(x => x is SO_SpellInfo) as SO_SpellInfo[];
            yield return null;
        }
        print(testers.Length);
        print(SData.Count);
        if (SData.Count > 0) foreach (var VARIABLE in SData)
        {
            print(VARIABLE);
        }
        
    }*/
}
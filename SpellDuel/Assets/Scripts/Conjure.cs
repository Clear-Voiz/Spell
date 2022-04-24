using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.Windows.Speech;

public class Conjure : MonoBehaviour
{
    private KeywordRecognizer _recognizer;
    private Transform _player1;
    //private Transform _player2;
    private Dictionary<string, Action> spellDic = new Dictionary<string, Action>();
    public Transform ori;
    public Speller speller1;
    private GameObject sparks;
    public Transform wand;
    private GameObject shield;
    private GameObject terra;
    private GameObject thunder;

    private StoreHouse storeHouse;

    //Actual spell
    private GameObject fireBall;

    private void Awake()
    {
        storeHouse = FindObjectOfType<StoreHouse>();
        _player1 = GameObject.Find("Player1").transform;
        sparks = Resources.Load("PS_sparks") as GameObject;
        shield = Resources.Load("Shield") as GameObject;
        terra = Resources.Load("Terra") as GameObject;
        fireBall = Resources.Load("PS_FireBall") as GameObject;
        thunder = Resources.Load("Thunder") as GameObject;
    }

    private void Start()
    {
        spellDic.Add("fire",Fire);
        spellDic.Add("llama",Fire);
        spellDic.Add("levitate",Levitate);
        spellDic.Add("levita",Levitate);
        spellDic.Add("shield",Shield);
        spellDic.Add("escudo",Shield);
        spellDic.Add("earth",Earth);
        spellDic.Add("tierra",Earth);
        spellDic.Add("thunder",Thunder);
        spellDic.Add("rayo",Thunder);
        spellDic.Add("vanish",Vanish);
        spellDic.Add("oculto",Vanish);
        //spellDic.Add("freeze",Freeze);
        //spellDic.Add("thunder",Thunder);

        _recognizer = new KeywordRecognizer(spellDic.Keys.ToArray());
        _recognizer.OnPhraseRecognized += Recognized;
        _recognizer.Start();
        Debug.Log(Application.systemLanguage);
    }
    
    
    private void Recognized(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        spellDic[speech.text].Invoke();
        CallSpeller(speech);
    }
    
    private void CallSpeller(PhraseRecognizedEventArgs speech)
    {
        var txt = speech.text;
        txt = txt.Substring(0, 1).ToUpper() + txt.Substring(1);
        speller1._renderer.text = txt;
        speller1.visible = true;
        speller1.secs = 0;
    }
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) Thunder();
        if (Input.GetMouseButtonDown(1)) Earth();
    }

    //SPELL DEFINITIONS
    private void Fire()
    {
        var shot = Instantiate(fireBall,ori.position + (ori.forward*1.2f),ori.rotation);
        Instantiate(sparks, ori.transform );
        //MasterServer
    }

    private void Levitate()
    {
        _player1.position += Vector3.up*3f;
    }
    private void Shield()
    {
        GameObject barrera = Instantiate(shield, _player1.position,Quaternion.identity);
        Destroy(barrera,1.5f);
    }

    private void Earth()
    {
        Instantiate(terra, new Vector3(ori.position.x+(ori.forward.x*3.5f),storeHouse.groundLevel,ori.position.z+(ori.forward.z*3.5f)), Quaternion.identity);
        //spike.transform.position = new Vector3(spike.transform.position.x, storeHouse.groundLevel, spike.transform.position.z);
    }

    private void Thunder()
    {
        RaycastHit hit;
        Physics.Raycast(ori.position, ori.forward, out hit);
        GameObject lightning = Instantiate(thunder, ori.position + (ori.forward*1.2f), ori.rotation);

        var spell = lightning.GetComponent<ThunderS>();
        
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                spell.Strike(hit.collider);
            }
            spell.range = hit.distance;
        }
        else
        {
            spell.range = 30f;
        }
    }

    private void Vanish()
    {
        
    }
}

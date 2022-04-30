using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
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
    private Transform _player1_mesh;
    private GameObject shield;
    private GameObject terra;
    private GameObject thunder;
    private GameObject mgkHit;
    private GameObject impulse;
    private Transform gameManager;
    private GameObject doom;
    public bool whisper;

    public StoreHouse storeHouse;

    //Actual spell
    private GameObject fireBall;

    private void Awake()
    {
        _player1 = GameObject.Find("Player1").transform;
        _player1_mesh = GameObject.Find("Player1_mesh").transform;
        
        sparks = Resources.Load("PS_sparks") as GameObject;
        shield = Resources.Load("Shield") as GameObject;
        terra = Resources.Load("Terra") as GameObject;
        fireBall = Resources.Load("PS_FireBall") as GameObject;
        thunder = Resources.Load("Thunder") as GameObject;
        mgkHit = Resources.Load("Hit") as GameObject;
        impulse = Resources.Load("Impulse") as GameObject;
        doom = Resources.Load("Doom") as GameObject;
        gameManager = transform.GetChild(0);
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
        spellDic.Add("hit",MagicHit);
        spellDic.Add("golpe", MagicHit);
        spellDic.Add("presto", Presto);
        spellDic.Add("impulse", Impulse);
        spellDic.Add("impulso", Impulse);
        spellDic.Add("doom", Doom);
        spellDic.Add("whisper", Whisper);
        spellDic.Add("susurro", Whisper);
        //spellDic.Add("freeze",Freeze);

        _recognizer = new KeywordRecognizer(spellDic.Keys.ToArray());
        _recognizer.OnPhraseRecognized += Recognized;
        _recognizer.Start();
//        Debug.Log(Application.systemLanguage);
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
        if (whisper)
            return;
        speller1.visible = true;
        speller1.secs = 0;
    }
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) Presto();
        if (Input.GetMouseButtonDown(1)) Impulse();
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
        Instantiate(shield, _player1_mesh.position,Quaternion.identity);
    }

    private void Earth()
    {
        Instantiate(terra, new Vector3(ori.position.x+(ori.forward.x*3.5f),storeHouse.groundLevel,ori.position.z+(ori.forward.z*3.5f)), Quaternion.identity);
    }

    private void Thunder()
    {
        Instantiate(thunder, ori.position + (ori.forward*1.2f), ori.rotation);
    }

    private void Vanish()
    {
        gameManager.gameObject.AddComponent<VanishS>();
    }

    private void MagicHit()
    {
        Instantiate(mgkHit, ori.position + (ori.forward * 1.2f), ori.rotation);
    }

    private void Impulse()
    {
        Instantiate(impulse,ori.position,Quaternion.identity);
    }

    private void Presto()
    {
        gameManager.gameObject.AddComponent<PrestoS>();
    }
    
    private void Doom()
    {
        Instantiate(doom,ori.position,ori.rotation);
    }

    private void Whisper()
    {
        gameManager.gameObject.AddComponent<WhisperS>();
    }
}

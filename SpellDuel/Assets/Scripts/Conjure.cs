using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
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

    //Actual spell
    private GameObject fireBall;

    private void Awake()
    {
        _player1 = GameObject.Find("Player1").transform;
        fireBall = Resources.Load("PS_FireBall") as GameObject;
        sparks = Resources.Load("PS_sparks") as GameObject;
        shield = Resources.Load("Shield") as GameObject;
        terra = Resources.Load("Terra") as GameObject;
    }

    private void Start()
    {
        spellDic.Add("fire",Fire);
        spellDic.Add("fuego",Fire);
        spellDic.Add("levitate",Levitate);
        spellDic.Add("levita",Levitate);
        spellDic.Add("shield",Shield);
        spellDic.Add("escudo",Shield);
        spellDic.Add("earth",Earth);
        spellDic.Add("tierra",Earth);
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
        if (Input.GetMouseButtonDown(0)) Fire();
    }

    //SPELL DEFINITIONS
    private void Fire()
    {
        var shot = Instantiate(fireBall,ori.position + (ori.forward/1.25f),ori.localRotation);
        Shoot shotScipt = shot.GetComponent<Shoot>();
        shotScipt.dir = ori;
        shotScipt.conjurer = gameObject;
        
        var rot = Quaternion.Euler(0f,0f,wand.rotation.z);
        GameObject sp = Instantiate(sparks, ori.position, rot);
        sp.transform.forward = ori.forward;

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
         GameObject spike = Instantiate(terra, _player1.position+(ori.forward*4f), Quaternion.identity);
        Debug.Log(_player1.position);
        Debug.Log(spike.transform.position);
        
        Destroy(spike,3f);
    }
}

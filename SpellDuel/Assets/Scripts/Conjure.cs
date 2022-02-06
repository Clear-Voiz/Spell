using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class Conjure : MonoBehaviour
{
    private KeywordRecognizer _recognizer;
    private Transform _player1;
    //private Transform _player2;
    private Dictionary<string, Action> spellDic = new Dictionary<string, Action>();
    public Transform ori;

    //Actual spell
    private GameObject fireBall;

    private void Awake()
    {
        _player1 = GameObject.Find("Player1").transform;
        fireBall = Resources.Load("PS_FireBall") as GameObject;
    }

    private void Start()
    {
        spellDic.Add("fire",Fire);
        spellDic.Add("fuego",Fire);
        spellDic.Add("levitate",Levitate);
        spellDic.Add("levita",Levitate);
        spellDic.Add("shield",Shield);
        spellDic.Add("escudo",Shield);
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
    }

    private void Fire()
    {
        var shot = Instantiate(fireBall,ori.position+ori.up,ori.transform.localRotation);
        shot.GetComponent<Shoot>().dir = ori;
    }

    private void Levitate()
    {
        _player1.position += Vector3.up;
    }
    private void Shield()
    {
        Instantiate(Resources.Load("Shield") as GameObject, _player1.position,Quaternion.identity);
    }
}

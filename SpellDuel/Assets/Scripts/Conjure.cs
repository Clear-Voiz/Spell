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
    private Dictionary<string, Action> spellDic = new Dictionary<string, Action>();
    private Vector3 dir;
    
    //Actual spell
    private GameObject fireBall;

    private void Awake()
    {
        _player1 = GameObject.Find("Player1").transform;
        fireBall = Resources.Load("fireBall") as GameObject;
    }

    private void Start()
    {
        spellDic.Add("fire",Fire);
        spellDic.Add("fuego",Fire);
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
        Instantiate(fireBall,_player1.transform.localPosition+_player1.transform.right,_player1.transform.rotation);
    }
    private void Patata()
    {
        Instantiate(fireBall,_player1.transform.localPosition+_player1.transform.right,_player1.transform.rotation);
    }
    
}

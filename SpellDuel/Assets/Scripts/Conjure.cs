using System;
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
    public Speller speller1;
    public Transform _player1_mesh;
    private Transform player2;
    private Transform gameManager;
    public bool whisper;
    public AimAt aimAt;
    public EffectsManager effectsManager;
    private HUD_Displayer _hudDisplayer;
    public SpellCosts cost;

    public StoreHouse SH;
    public Stats[] stats;

    private void Awake()
    {
        _player1 = GameObject.FindWithTag("Player").transform;
        _player1_mesh = GameObject.Find("foxy_170cm").transform;
        player2 = GameObject.Find("Player2").transform;
        gameManager = transform.GetChild(0);
        var weapon = _player1.transform.GetChild(2);
        aimAt = weapon.GetComponent<AimAt>();
        effectsManager = _player1.GetComponent<EffectsManager>();
        _hudDisplayer = GetComponent<HUD_Displayer>();
        stats = FindObjectsOfType<Stats>();
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
        spellDic.Add("condena", Doom);
        spellDic.Add("whisper", Whisper);
        spellDic.Add("susurro", Whisper);
        spellDic.Add("ice",Shards);
        spellDic.Add("hielo",Shards);
        spellDic.Add("darkness", Darkness);
        spellDic.Add("oscuridad",Darkness);
        spellDic.Add("paralysis",Paralysis);
        spellDic.Add("paralisis",Paralysis);
        spellDic.Add("water",Water);
        spellDic.Add("Agua", Water);
        spellDic.Add("check",Check);
        spellDic.Add("Jaque", Check);
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
        if (Input.GetMouseButtonDown(0)) Fire();
        if (Input.GetMouseButtonDown(1)) Shards();
    }

    //SPELL DEFINITIONS
    private void Fire()
    {
        if (stats[0].mt < stats[0].maxMt.Value) _hudDisplayer.Mt += 5;
        
        var shot = Instantiate(SH.fireBall,ori.position + (ori.forward*1.2f),ori.rotation);
        Instantiate(SH.sparks, ori.transform );
        //MasterServer
    }

    private void Levitate()
    {
        _player1.position += Vector3.up*3f;
    }
    private void Shield()
    {
        Instantiate(SH.shield, _player1_mesh.position,Quaternion.identity);
    }

    private void Earth()
    {
        Instantiate(SH.terra, new Vector3(ori.position.x+(ori.forward.x*3.5f),SH.groundLevel,ori.position.z+(ori.forward.z*3.5f)), Quaternion.identity);
    }

    private void Thunder()
    {
        Instantiate(SH.thunder, ori.position + (ori.forward*1.2f), ori.rotation);
    }

    private void Vanish()
    {
        new SVanish(this);
    }

    private void MagicHit()
    {
        Instantiate(SH.mgkHit, ori.position + (ori.forward * 1.2f), ori.rotation);
    }

    private void Impulse()
    {
        Instantiate(SH.impulse,ori.position,Quaternion.identity);
    }

    private void Presto()
    {
        //gameManager.gameObject.AddComponent<PrestoS>();
        new SPresto();
    }
    
    private void Doom()
    {
        Instantiate(SH.doom,ori.position,ori.rotation);
    }

    private void Whisper()
    {
        new SWhisper();
        
    }

    private void Darkness()
    {
        //new SDarkness();
    }

    private void Shards()
    { 
        new SIce();
    }

    private void Paralysis()
    {
        Instantiate(SH.paralysis,ori.position,ori.rotation);
    }
    
    private void Water()
    {
        new SWater();
        
    }

    private void Check()
    {
        var place = Vector3.up * 3f + player2.transform.position;
        Instantiate(SH.checker,place,Quaternion.identity);
    }

}

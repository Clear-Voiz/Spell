using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using FishNet.Connection;
using FishNet.Object;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class Conjure : NetworkBehaviour
{
    private KeywordRecognizer _recognizer;
    //private Transform _player2;
    private Dictionary<string, Action> spellDic = new Dictionary<string, Action>();
    public Transform ori;
    public Speller speller;
    public Transform _player1_mesh;
    private Transform enemy;
    public bool whisper;
    public AimAt aimAt;
    public EffectsManager effectsManager;
    private HUD_Displayer _hudDisplayer;
    public SpellCosts cost;
    public Transform middlePoint;
    private Timers tim;

    public StoreHouse SH;
    public Stats stats;

    private void Awake()
    {
        effectsManager = GetComponent<EffectsManager>();
        _hudDisplayer = FindObjectOfType<HUD_Displayer>();
        stats = GetComponent<Stats>();
        speller = FindObjectOfType<Speller>();
        tim = new Timers(1);
    }

    /*public override void OnStartClient()
    {
        base.OnStartClient();
        if (GameManager.Instance.players.Count<1) return;
        foreach (var player in GameManager.Instance.players)
            if (!player.IsOwner)
            {
                enemy = player.controlledPawn.transform;
                break;
            }
    }*/

    public override void OnStartClient()
    {
        base.OnStartClient();

        if (!IsOwner) return;
        /*if (spellDic.Count >0) spellDic.Clear();
        
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
        _recognizer.Start();*/
        
        
//        Debug.Log(Application.systemLanguage);

        /*if (GameManager.Instance.players.Count<1) return;
        foreach (var player in GameManager.Instance.players)
            if (!player.IsOwner)
            {
                if (player.controlledPawn.transform == null) break;
                enemy = player.controlledPawn.transform;
                break;
            }*/
        
       // Debug.Log(OwnerId + ": " + GameManager.Instance.players.Count);
        
        
    }


    private void Recognized(PhraseRecognizedEventArgs speech)
    {
        if (!IsOwner) return;
        Debug.Log(speech.text);
        spellDic[speech.text].Invoke();
        CallSpeller(speech);
    }
    
    private void CallSpeller(PhraseRecognizedEventArgs speech)
    {
        var txt = speech.text;
        txt = txt.Substring(0, 1).ToUpper() + txt.Substring(1);
        speller._renderer.text = txt;
        if (whisper)
            return;
        speller.visible = true;
        speller.secs = 0;
    }
    
    private void Update()
    {
        if (!IsOwner) return;
        //if (Input.GetMouseButtonDown(0)) Check();
        if (Input.GetMouseButtonDown(1)) Fire();
        //tim.Timer(1f, tim.alarm[0], MyEnemy);

    }

    private void MyEnemy()
    {
        for (int i=0; i< GameManager.Instance.players.Count;i++)
        {
            if (!GameManager.Instance.players[i].IsOwner)
            {
                Debug.Log(i);
                Debug.Log(GameManager.Instance.players[i].controlledPawn);
                enemy = GameManager.Instance.players[i].controlledPawn.transform;
                break;
            }
        }
    }

    //SPELL DEFINITIONS
    [ServerRpc]
    private void Fire()
    {
        //if (stats.mt < stats.maxMt.Value) _hudDisplayer.Mt += 5;
        Debug.Log("Launched fire");
        
        var shot = Instantiate(SH.fireBall,ori.position + (ori.forward*1.2f),aimAt.rot);
        shot._conjure = this;
        if (shot == null) return;
        Spawn(shot.gameObject,Owner);
        var vfx = Instantiate(SH.sparks, ori.position, ori.rotation);
        if (vfx == null) return;
        
        Spawn(vfx,Owner);

    }

    private void Levitate()
    {
        transform.position += Vector3.up*3f;
    }

    [ServerRpc]
    private void Shield()
    {
        ShieldS shield = Instantiate(SH.shield, middlePoint.position,Quaternion.identity);
        Spawn(shield.gameObject,Owner);
    }

    [ServerRpc]
    private void Earth()
    {
        var earth = Instantiate(SH.terra, new Vector3(ori.position.x+(ori.forward.x*3.5f),SH.groundLevel,ori.position.z+(ori.forward.z*3.5f)), Quaternion.identity);
        earth._conjure = this;
        Spawn(earth.gameObject,Owner);
    }

    [ServerRpc]
    private void Thunder()
    {
        var thunder = Instantiate(SH.thunder, ori.position + (ori.forward*1.2f), ori.rotation);
        Spawn(thunder.gameObject,Owner);
    }

    private void Vanish()
    {
        new SVanish(this);
    }

    [ServerRpc]
    private void MagicHit() //
    {
        
        HitS hit = Instantiate(SH.mgkHit, ori.position + (ori.forward * 1.2f), ori.rotation);
       hit._conjure = this;
       Spawn(hit.gameObject,Owner);
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
        DoomS doom = Instantiate(SH.doom,ori.position,ori.rotation);
        doom._conjure = this;
        Spawn(doom.gameObject, Owner);
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

    [ServerRpc]
    private void Check()
    {
        MyEnemy();
        if (enemy == null) return;
        var place = Vector3.up * 3f + enemy.position;
        CheckS checker = Instantiate(SH.checker,place,Quaternion.identity);
        Spawn(checker.gameObject,Owner);
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using FishNet.Connection;
using FishNet.Object;
using FishNet.Object.Synchronizing;
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
    [SyncVar] public Transform enemy;
    public bool whisper;
    public AimAt aimAt;
    public EffectsManager effectsManager;
    private HUD_Displayer _hudDisplayer;
    public SpellCosts cost;
    public Transform middlePoint;
    private Timers tim;
    private Action channelled;
    [SyncVar] public float cooldown;
    public bool recastable = true;

    public StoreHouse SH;
    public Stats stats;
    public ConfidenceLevel confidence = ConfidenceLevel.Medium;

    private void Awake()
    {
        effectsManager = GetComponent<EffectsManager>();
        _hudDisplayer = FindObjectOfType<HUD_Displayer>();
        stats = GetComponent<Stats>();
        speller = FindObjectOfType<Speller>();
        tim = new Timers(1);
        channelled = Water;
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        if (!IsOwner) return;
        if (spellDic.Count >0) spellDic.Clear();
        
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
        

        if (_recognizer == null)
        {
            _recognizer = new KeywordRecognizer(spellDic.Keys.ToArray(),confidence);
            _recognizer.OnPhraseRecognized += Recognized;
            _recognizer.Start();
        }

        MyEnemy(Owner);
        
    }

    private void OnDisable()
    {
        if(_recognizer !=null && _recognizer.IsRunning)
        {_recognizer.OnPhraseRecognized -= Recognized;
         _recognizer.Stop();
         _recognizer.Dispose();}
    }

    [ServerRpc]
    private void MyEnemy(NetworkConnection conn)
    {
        for (int i=0; i< GameManager.Instance.players.Count;i++)
        {
            if (GameManager.Instance.players[i].Owner != conn)
            {
                
                /*Transform ene*/
                enemy = GameManager.Instance.players[i].controlledPawn.transform;
                //ReceiveEnemy(conn,ene);
                
                break;
            }
        }
    }


    private void Recognized(PhraseRecognizedEventArgs speech)
    {
        if (!IsOwner) return;
        Debug.Log(speech.text);
        Action spell = spellDic[speech.text];

        if (channelled != spell)
        {
            channelled = spell;
            spell.Invoke();
        }
        else
        {
            if (recastable==false) return;
            spell.Invoke();
        }
        recastable = false;
        tim.alarm[0] = 0f;
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
        tim.alarm[0] = tim.Timer(cooldown, tim.alarm[0], ()=>recastable = true);
        
        //if (Input.GetMouseButtonDown(0)) Check();
        if (!recastable) return;
        if (Input.GetMouseButtonDown(1))
        {
            channelled.Invoke();
            recastable = false;
            tim.alarm[0] = 0f;
        }
        
    }

    

    //SPELL DEFINITIONS
    [ServerRpc]
    private void Fire()
    {
        //if (stats.mt < stats.maxMt.Value) _hudDisplayer.Mt += 5;
        Debug.Log("Launched fire");
        
        FireS shot = Instantiate(SH.fireBall,ori.position + (ori.forward*1.2f),aimAt.rot);
        shot._conjure = this;
        cooldown = shot.cooldown;
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
        cooldown = shield.cooldown;
        Spawn(shield.gameObject,Owner);
    }

    [ServerRpc]
    private void Earth()
    {
        GroundS earth = Instantiate(SH.terra, new Vector3(ori.position.x+(ori.forward.x*3.5f),SH.groundLevel,ori.position.z+(ori.forward.z*3.5f)), Quaternion.identity);
        earth._conjure = this;
        cooldown = earth.cooldown;
        Spawn(earth.gameObject,Owner);
    }

    [ServerRpc]
    private void Thunder()
    {
        ThunderS thunder = Instantiate(SH.thunder, ori.position + (ori.forward*1.2f), ori.rotation);
        cooldown = thunder.cooldown;
        thunder._conjure = this;
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
       cooldown = hit.cooldown;
       Spawn(hit.gameObject,Owner);
    }

    [ServerRpc]
    private void Impulse()
    {
        ImpulseS impulse = Instantiate(SH.impulse,ori.position,Quaternion.identity);
        impulse._conjure = this;
        cooldown = impulse.cooldown;
        Spawn(impulse.gameObject,Owner);
    }

    [ServerRpc]
    private void Presto()
    {
        PrestoS haste = Instantiate(SH.presto, ori.position, ori.rotation);
        haste._conjure = this;
        cooldown = 5f;
        Spawn(haste.gameObject,Owner);
        effectsManager.AddBuff(haste);
    }
    
    [ServerRpc]
    private void Doom()
    {
        DoomS doom = Instantiate(SH.doom,ori.position,ori.rotation);
        doom._conjure = this;
        cooldown = doom.cooldown;
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
    
    [ServerRpc]
    private void Shards()
    { 
         BlizzardS blizzard = Instantiate(SH.blizzard,ori.position,Quaternion.identity);
         blizzard._conjure = this;
         cooldown = blizzard.cooldown;
         Spawn(blizzard.gameObject,Owner);
    }

    private void Paralysis()
    {
      //SParalysis paralysis =  Instantiate(SH.paralysis,ori.position,ori.rotation);
    }
    
    [ServerRpc]
    private void Water()
    {
        SWater water = Instantiate(SH.water,ori.position,ori.rotation);
        water._conjure = this;
        cooldown = 1f;
        Spawn(water.gameObject,Owner);
        effectsManager.AddBuff(water);
        
    }

    [ServerRpc]
    private void Check()
    {
        if (enemy == null) return;
        var place = Vector3.up * 3f + enemy.position;
        CheckS checker = Instantiate(SH.checker,place,Quaternion.identity);
        cooldown = checker.cooldown;
        Spawn(checker.gameObject,Owner);
    }

}

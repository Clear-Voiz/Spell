using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using FishNet.Connection;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.Windows.Speech;

public class Conjure : NetworkBehaviour
{
    private KeywordRecognizer _recognizer;
    //private Transform _player2;
    private Dictionary<string, Action> spellDic = new Dictionary<string, Action>();
    public Transform ori;
    [HideInInspector] public Speller speller;
    [HideInInspector] public SpellListener spellListener;
    public Transform playerMesh;
    [HideInInspector] [SyncVar] public Transform enemy;
    [HideInInspector] public bool whisper;
    public AimAt aimAt;
    public EffectsManager effectsManager;
    public PawnStateManager stateManager;
    private HUD_Displayer _hudDisplayer;
    public SpellCosts cost;
    public Transform middlePoint;
    private Timers tim;
    private Action channelled;
    [HideInInspector][SyncVar] public float cooldown;
    [HideInInspector] public bool recastable = true;

    public StoreHouse SH;
    public Stats stats;
    [HideInInspector] public ConfidenceLevel confidence = ConfidenceLevel.Medium;

    private void Awake()
    {
        effectsManager = GetComponent<EffectsManager>();
        _hudDisplayer = FindObjectOfType<HUD_Displayer>();
        stats = GetComponent<Stats>();
        speller = FindObjectOfType<Speller>();
        tim = new Timers(1);
        channelled = Impulse;
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        if (!IsOwner) return;
        if (spellDic.Count >0) spellDic.Clear();
        
        spellDic.Add("fuego",Fire);
        spellDic.Add("levita",Levitate);
        spellDic.Add("escudo",Shield);
        spellDic.Add("tierra",Earth);
        spellDic.Add("rayo",Thunder);
        spellDic.Add("oculto",Vanish);
        spellDic.Add("golpe", MagicHit);
        spellDic.Add("presto", Presto);
        spellDic.Add("impulso", Impulse);
        spellDic.Add("condena", Doom);
        spellDic.Add("susurro", Whisper);
        spellDic.Add("hielo",Shards);
        spellDic.Add("oscuridad",Darkness);
        spellDic.Add("parálisis",Paralysis);
        spellDic.Add("agua", Water);
        spellDic.Add("jaque", Check);
        
        SpellListener.OnListenerEnabled += listener =>
        {
            spellListener = listener;
            spellListener.conjure = this;
        };
        


        /*if (_recognizer == null)
        {
            _recognizer = new KeywordRecognizer(spellDic.Keys.ToArray(),confidence);
            _recognizer.OnPhraseRecognized += Recognized;
            _recognizer.Start();
        }*/

        MyEnemy(Owner);
        
        
    }

    /*private void OnDisable()
    {
        if(_recognizer !=null && _recognizer.IsRunning)
        {_recognizer.OnPhraseRecognized -= Recognized;
         _recognizer.Stop();
         _recognizer.Dispose();}
    }*/

    

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


    public void Recognized(string speech)
    {
        if (!IsOwner) return;
        Debug.Log(speech);
        Action spell = spellDic[speech];

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
    
    private void CallSpeller(string speech)
    {
        speech = speech.Substring(0, 1).ToUpper() + speech.Substring(1);
        speller._renderer.text = speech;
        if (whisper)
            return;
        speller.visible = true;
        speller.secs = 0;
    }
    
    private void Update()
    {
        if (!IsOwner) return;
        if (Input.GetKeyDown(KeyCode.L))
        {
            Slow();
        }
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
        stateManager.SwitchBattleState(stateManager.shortAndFast);
        //Cast(SH.fireBall,0.3f,SH.sparks);
        FireS shot = Instantiate(SH.fireBall,ori.position + (ori.forward*1.2f),aimAt.rot);
        shot._conjure = this;
        cooldown = shot.cooldown;
        if (shot == null) return;
        Spawn(shot.gameObject,Owner);
        var vfx = Instantiate(SH.sparks, ori.position, ori.rotation);
        if (vfx == null) return;
        
        Spawn(vfx,Owner);
    }

    [ServerRpc]
    private void Slow()
    {
        SSlow slow = Instantiate(SH.slow, ori.position, ori.rotation);
        slow._conjure = this;
        cooldown = 5f;
        Spawn(slow.gameObject,Owner);
        effectsManager.AddBuff(slow);
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
        stateManager.SwitchBattleState(stateManager.objectLift);
        GroundS earth = Instantiate(SH.terra, new Vector3(ori.position.x+(ori.forward.x*3.5f),SH.groundLevel,ori.position.z+(ori.forward.z*3.5f)), Quaternion.identity);
        earth._conjure = this;
        cooldown = earth.cooldown;
        Spawn(earth.gameObject,Owner);
    }

    
    private void Thunder()
    {
        stateManager.SwitchState(stateManager.stab);
        Cast(SH.thunder,0.85f);
    }

    private void Vanish()
    {
        VanishS vanish = Instantiate(SH.vanish);
        vanish._conjure = this;
        vanish.cooldown = 5f;
        Spawn(vanish.gameObject,Owner);

    }

  
    private void MagicHit() //
    {
        stateManager.SwitchState(stateManager.shortAndFast);
        Cast(SH.mgkHit, 0.5f);
    }

 
    private void Impulse()
    {
        //stateManager.SwitchState(stateManager.objectLift);
        Cast(SH.impulse, 0.01f);
        
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
        stateManager.SwitchState(stateManager.shortAndFast);
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
        stateManager.SwitchState(stateManager.objectLift);
        var place = Vector3.up * 3f + enemy.position;
        CheckS checker = Instantiate(SH.checker,place,Quaternion.identity);
        cooldown = checker.cooldown;
        Spawn(checker.gameObject,Owner);
    }
    
    [ServerRpc]
    public async void Cast(Spell spell,  float delay) // Vector3 position, Quaternion rotation,
    {
        float trueDelay = delay * 1000f;
        await Task.Delay((int) trueDelay);
        var magic = Instantiate(spell, ori.position, ori.rotation);
        magic._conjure = this;
        cooldown = magic.cooldown;
        Spawn(magic.gameObject,Owner);
    }
    [ServerRpc]
    public async void Cast(Spell spell,  float delay, GameObject visualEffect) // Vector3 position, Quaternion rotation,
    {
        int trueDelay = (int)(delay * 1000f);
        await Task.Delay(trueDelay); 
        var magic = Instantiate(spell, ori.position, ori.rotation);
        magic._conjure = this;
        cooldown = magic.cooldown;
        Debug.Log(cooldown);
        if (magic == null) return; 
            Spawn(magic.gameObject,Owner);
        GameObject vFX = Instantiate(visualEffect,ori.position, ori.rotation); 
        if (visualEffect != null)  return;
            Spawn(vFX,Owner);
    }
    
    
    [ServerRpc]
    public async void Cast(AlterSpell spell, Vector3 position, Quaternion rotation,bool isBuff, float delay)
    {
        float trueDelay = delay * 1000f;
        await Task.Delay((int) trueDelay);
        var magic = Instantiate(spell, position, rotation);
        magic._conjure = this;
        cooldown = magic.cooldown;
        Spawn(magic.gameObject,Owner);
        if (isBuff)
        {
            effectsManager.ActiveBuffs.Add(magic);
        }
        else
        
        {
            effectsManager.ActiveDebuffs.Add(magic);
        }
    }

}

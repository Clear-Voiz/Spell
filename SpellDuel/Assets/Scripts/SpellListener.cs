using System;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class SpellListener : MonoBehaviour
{
    private KeywordRecognizer _recognizer;
    private string[] spellDic;
    [HideInInspector] public Speller speller;
    [HideInInspector] public bool whisper;
    [HideInInspector] public ConfidenceLevel confidence = ConfidenceLevel.Medium;
    public Conjure conjure;
    public static event Action<SpellListener>  OnListenerEnabled;

    private void Awake()
    {
        Pawn.OnFirstObjectSpawned += GetConjure;
    }

    private void OnEnable()
    {
        if (speller == null) speller = FindObjectOfType<Speller>();
        
        if (_recognizer == null && spellDic == null)
        {
            spellDic = new[]
            {
                "fuego", "levita", "escudo", "tierra", "rayo", "oculto", "golpe", "presto", "impulso", "condena",
                "susurro", "hielo", "oscuridad", "parálisis", "agua", "jaque"
            };
            _recognizer = new KeywordRecognizer(spellDic,confidence);
            _recognizer.OnPhraseRecognized += Recognized;
            _recognizer.Start();
            Debug.Log("listening");
            Debug.Log(spellDic[12]);
            
            OnListenerEnabled?.Invoke(this);
        }
        
    }
    
    private void OnDisable()
    {
        if(_recognizer !=null && _recognizer.IsRunning)
        {_recognizer.OnPhraseRecognized -= Recognized;
         _recognizer.Stop();
         _recognizer.Dispose();}

        Pawn.OnFirstObjectSpawned -= GetConjure;
    }

    /*public void OnReadyToListen()
    {
        spellDic = new[]
        {
            "fuego", "levita", "escudo", "tierra", "rayo", "oculto", "golpe", "presto", "impulso", "condena",
            "susurro", "hielo", "oscuridad", "parálisis", "agua", "jaque"
        };
        _recognizer = new KeywordRecognizer(spellDic,confidence);
        _recognizer.OnPhraseRecognized += Recognized;
        _recognizer.Start();
        Debug.Log("listening");
    }*/
    

    private void Recognized(PhraseRecognizedEventArgs args)
    {
        conjure.Recognized(args.text);
    }

    private void GetConjure(Transform trans)
    {
        conjure = trans.GetComponent<Conjure>();
        trans.TryGetComponent(out conjure);
        Debug.Log("well... we tried");
        
        //EXTRA
    }
}

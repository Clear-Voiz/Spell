using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GoToMicro : MonoBehaviour
{
    public GameObject butt;
    public int pos;
    public GameObject hanger;
    public GameObject configCan;
    public List<GameObject> butCol;


    public void CreateMicroButtons()
    {
        foreach (var device in Microphone.devices)
        {
            var ob = Instantiate(butt,hanger.transform);
            ob.GetComponentInChildren<TextMeshProUGUI>().text = device;
            butCol.Add(ob);
            SelectedMicro selMi = ob.AddComponent<SelectedMicro>();
            selMi.dev = device;
            Button butter = ob.GetComponent<Button>();
            butter.onClick.AddListener(selMi.SelectMicro);

        }
        
        var obj = Instantiate(butt,hanger.transform);
        obj.GetComponentInChildren<TextMeshProUGUI>().text = "Back";
        var Returner = obj.AddComponent<GoBack>();
        Returner.ob = configCan;
        Returner.rubbish = butCol;
        obj.GetComponent<Button>().onClick.AddListener(Returner.Return);
        configCan.SetActive(false);
        butCol.Add(obj);
    }

    public void GoBack(GameObject ob, List<GameObject> rubbish)
    {
        foreach (var button in rubbish)
        {
            Destroy(button);
        }
        rubbish.Clear();
        ob.SetActive(true);
    }
}

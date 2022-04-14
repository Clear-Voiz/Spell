using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class nodes : MonoBehaviour,IPointerClickHandler
{
   public SpellTree.Spells id;
   public nodes dependant;
   public nodes nextNode;
   public int investedPoints; //Points invested in this magic level
   public int requiredPoints; //Required points to upgrade this magic to the next level
   public SpellTree.State State;
   private SpellTree _spellTree;
   private GameObject seal; 
   private string colActive;
   private string colInactive;
   public string[] definition;
   private string[] textFormat;
   public TextMeshProUGUI txter;
   public int page { get; set; }
   private TextMeshProUGUI pointsShower;

   public int InvestedPoints
   {
       get
       {
           return investedPoints;
       }
       set
       {
           pointsShower.text = value + "/5";
           
           if (value == 0)
           {
               State = SpellTree.State.Unlocked;
               _spellTree.activeSpells.Remove((int) id);
           }
           investedPoints = value;
       }
   }


   private void Awake()
   {
       definition = new []{"Fire</color></b>: casts a flame controllable with your wand.","Firing</color></b>: Increased size and damage.","Fired</color></b>: Explodes when colliding or manually by left-pressing your wand."};
       textFormat = new string[3];
       _spellTree = FindObjectOfType<SpellTree>();
       pointsShower = GetComponentInChildren<TextMeshProUGUI>();
       id = SpellTree.Spells.Fire;
   }
   private void Start()
   {
       State = SpellTree.State.Unlocked;
       colActive = "#ff0000ff";
       colInactive = "#800000ff";
       requiredPoints = 2;

       txter.text = Redefine();
       if (State == SpellTree.State.Locked)
       {
           gameObject.SetActive(false);
       }
   }

   //constructor
   public nodes(SpellTree.Spells ID, nodes Dependant, nodes NextNode, SpellTree.State state)
   {
       id = ID;
       dependant = Dependant;
       nextNode = NextNode;
       State = state;
   }

   public void OnPointerClick(PointerEventData eventData)
   {
       if (eventData.button == PointerEventData.InputButton.Left)
       {
           if (State != SpellTree.State.Locked && (dependant == null || dependant.State == SpellTree.State.Active))
           {
               if (_spellTree.leftPoints > 0 && InvestedPoints<5)
               {
                   State = SpellTree.State.Active;
                   _spellTree.leftPoints -= 1;
                   InvestedPoints += 1;
                   if (InvestedPoints == 3 || InvestedPoints == 5) id += 1;
                   txter.text = Redefine();
                   _spellTree.leftPointsTxt.text = "Available points: " + _spellTree.leftPoints;
                   _spellTree.activeSpells.Add((int)id);
                   //seal = Instantiate(_spellTree.seal,transform);
                   //seal.transform.position = transform.position;
               }
           }
       }
       else if (eventData.button == PointerEventData.InputButton.Right)
       {
           if (State == SpellTree.State.Active)
           {
               _spellTree.leftPoints += 1;
               InvestedPoints -= 1;
               if (InvestedPoints == 2 || InvestedPoints == 4) id -= 1;
               txter.text = Redefine();
               _spellTree.leftPointsTxt.text = "Available points: " + _spellTree.leftPoints;
               GameObject trash = seal;
               seal = null;
               Destroy(trash);
           }
       }
   }
   private string Redefine()
   {
       string temp;
       for (int i=0;i<textFormat.Length;i++)
       {
           if (i == (int) id && State == SpellTree.State.Active)
           {
               textFormat[i] = $"<b><color={colActive}>";
           }
           else
           {
               textFormat[i] = $"<b><color={colInactive}>";
           }
       }
       temp = textFormat[0] + definition[0] + "\n" + textFormat[1] + definition[1] + "\n" + textFormat[2] + definition[2];
       return temp;
   }
}

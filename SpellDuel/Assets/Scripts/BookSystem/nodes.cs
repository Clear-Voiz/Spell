using System;
using System.Collections;
using System.Collections.Generic;
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
   [SerializeField] private string col = "#ff0000ff";
   private string definition;

   public String Col
   {
       get
       {
           if (State == SpellTree.State.Active)
           {
               col = "#ff0000ff";
           }
           else
           {
               col = "#800000ff";
           }
           return col;
       }
       set
       {
           col = value;
       }
   }
   

   private void Awake()
   {
       _spellTree = FindObjectOfType<SpellTree>();
   }

   //constructor
   public nodes(SpellTree.Spells ID, nodes Dependant, nodes NextNode, SpellTree.State state)
   {
       id = ID;
       dependant = Dependant;
       nextNode = NextNode;
       State = state;
   }
   private void Start()
   {
       State = SpellTree.State.Unlocked;
      if (State == SpellTree.State.Locked)
      {
            gameObject.SetActive(false);
      }
   }

   public void OnPointerClick(PointerEventData eventData)
   {
       if (eventData.button == PointerEventData.InputButton.Left)
       {
           if (State == SpellTree.State.Unlocked && (dependant == null) || (dependant.State == SpellTree.State.Active))
           {
               if (_spellTree.leftPoints > 0)
               {
                   State = SpellTree.State.Active;
                   _spellTree.leftPoints -= 1;
                   print(id);
                   _spellTree.leftPointsTxt.text = "Available points: " + _spellTree.leftPoints;
                   _spellTree.activeSpells.Add((int)id);
                   seal = Instantiate(_spellTree.seal,transform); //_spellTree.body.
                   seal.transform.position = transform.position;
               }
           }
       }
       else if (eventData.button == PointerEventData.InputButton.Right)
       {
           if (State == SpellTree.State.Active)
           {
               State = SpellTree.State.Unlocked;
               _spellTree.leftPoints += 1;
               _spellTree.leftPointsTxt.text = "Available points: " + _spellTree.leftPoints;
               _spellTree.activeSpells.Remove((int) id);
               GameObject trash = seal;
               seal = null;
               Destroy(trash);
           }
       }
   }
}

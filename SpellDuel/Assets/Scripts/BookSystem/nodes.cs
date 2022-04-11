using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class nodes : MonoBehaviour
{
   public SpellTree.Spells id;
   public nodes dependant;
   public nodes nextNode;
   public int investedPoints;
   public SpellTree.State State;

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

   public void Activation()
   {
       if (State == SpellTree.State.Unlocked)
       {
           State = SpellTree.State.Active;
           print(id);
       }
   }
}

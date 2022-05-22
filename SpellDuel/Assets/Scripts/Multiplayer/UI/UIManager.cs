using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class UIManager : MonoBehaviour
{
   public static UIManager Instance {get; private set;}

   [SerializeField] private View[] views;


   private void Awake()
   {
      Instance = this;
   }

   public void Initialize()
   {
      foreach (var view in views)
      {
         view.Initialize();
      }
   }

   public void Show<T>() where T : View
   {
      foreach (var view in views)
      {
         view.gameObject.SetActive(view is T);
      }      
   }
}

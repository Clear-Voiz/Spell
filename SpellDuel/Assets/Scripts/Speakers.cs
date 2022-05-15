using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Speakers : MonoBehaviour
{
   public AudioSource audioSource;

   private void Awake()
   {
      audioSource = GetComponent<AudioSource>();
   }

   private void Start()
   {
      print(Microphone.devices[0]);
      audioSource.clip = Microphone.Start(Microphone.devices[0], true, 10, 44100);
//      audioSource.Play();
   
   }


   private void Update()
   {
      
   }
}

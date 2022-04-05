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
      audioSource.clip = Microphone.Start("Micrófono (Realtek High Definition Audio)", true, 10, 44100);
      audioSource.Play();
   }


   private void Update()
   {
      
   }
}

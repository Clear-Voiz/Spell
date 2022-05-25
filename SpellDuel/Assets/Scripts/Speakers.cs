using System.Text;
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
      if (Microphone.devices.Length > 0)
      {
         print(Microphone.devices[0]);
         if (audioSource.clip == null)
            audioSource.clip = Microphone.Start(Microphone.devices[0], true, 10, 44100);
      }
      else
      {
         StringBuilder warning = new StringBuilder("No devices connected");
         print(warning.ToString());
      }
//      audioSource.Play();
   
   }


   private void Update()
   {
      
   }
}

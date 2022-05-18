using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LocalPoints : MonoBehaviour
{
   public float duration;
   private TextMeshPro tmp;
   public int points;
   private Vector3 startPos;
   private Vector3 endPos;
   private float elapsedTime;
   private float completePercent;

   private void Awake()
   {
      tmp = GetComponent<TextMeshPro>();
   }

   private void Start()
   {
      duration = 1f;
      points = 2;
      Destroy(gameObject,2f);
      tmp.text = $"tech <size=26><i>{points}</size></i>p";
      Globs.Xp += Mathf.RoundToInt(points * Globs.xpGain.Value);
      print(Globs.Xp);
      startPos = transform.position;
      endPos = startPos + new Vector3(0f, 1f, 0f);
   }

   private void Update()
   {
      elapsedTime += Time.deltaTime;
      completePercent = elapsedTime / duration;
      transform.position = Vector3.Lerp(startPos, endPos, completePercent);
      
      //if it doesn't always look at the camera set the transform.forward to the camera forward
   }
}

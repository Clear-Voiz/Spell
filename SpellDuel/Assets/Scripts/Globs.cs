using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Globs
{
   public static string mainMicro;
   public static string hechizo1;
   public static string hechizo2;
   public const int totalSpells = 1;
   public static float res;

   #region playerStats

   public static int lvl = 7;
   public static CharacterStat mgk = new CharacterStat(6f);
   public static CharacterStat xpGain = new CharacterStat(1f); //this will store the amount of xp multiplier. Add numbers between 0 and 1 to modifier list. Although percentage treat as flat
   public static CharacterStat mgkDef = new CharacterStat(2f);
   public static CharacterStat maxHp = new CharacterStat(10f);
   public static CharacterStat hp = new CharacterStat(maxHp.Value);
   public static CharacterStat maxMp = new CharacterStat(12f);
   public static CharacterStat mp = new CharacterStat(maxMp.Value);
   public static CharacterStat spd = new CharacterStat(3f);

   public static CharacterStat fireRes = new CharacterStat(1f);
   public static CharacterStat thunderRes = new CharacterStat(1f);
   public static CharacterStat iceRes = new CharacterStat(1f);
   public static CharacterStat lightRes = new CharacterStat(1f);
   public static CharacterStat darkRes = new CharacterStat(1f);

   public static int maxXp = 10;
   public const float ScaleFactor = 1.25f;
   public static int xp;
   public static int Xp
   {
      get => xp;
      set
      {
         if (value >= maxXp)
         {
            xp = value-maxXp;
            maxXp = Mathf.RoundToInt(maxXp*ScaleFactor);
            lvl += 1;
            Debug.Log("current lvl: "+lvl);

         }
         else
         {
            xp = value;
         }
      }
   }
   
   //public static float

   

   #endregion
   

   public static HashSet<int> activeSpells = new HashSet<int>();
}

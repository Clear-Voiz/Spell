using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Globs
{
   public static string mainMicro;
   public static string hechizo1;
   public static string hechizo2;
   public const int totalSpells = 1;

   public static HashSet<int> activeSpells = new HashSet<int>();
}

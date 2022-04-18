using System;
using System.Collections.Generic;

public class CharacterStat
{
   public float baseValue;
   
   public float Value
   {
      get 
      {
         if (isDirty)
         {
            _value = CalculateFinalValue();
            isDirty = false;
         }
         return _value;
      }
   }

   private bool isDirty = true;
   private float _value;
   
   private readonly List<StatModifier> statModifiers;

   public CharacterStat(float baseVal)
   {
      baseValue = baseVal;
      statModifiers = new List<StatModifier>();
   }
   
   public void AddModifier(StatModifier mod)
   {
      isDirty = true;
      statModifiers.Add(mod);
   }

   public bool RemoveModifier(StatModifier mod)
   {
      isDirty = true;
      return statModifiers.Remove(mod);
   }

   private float CalculateFinalValue()
   {
      float finalValue = baseValue;
      float percentAccumul = 1f;

      for (int i = 0; i < statModifiers.Count; i++)
      {
         if (statModifiers[i].Type == modiType.Flat)
         {finalValue += statModifiers[i].value;}
         else if (statModifiers[i].Type == modiType.Percent)
         {
            percentAccumul += statModifiers[i].value;
         }
      }

      finalValue *= percentAccumul;

      return (float)Math.Round(finalValue,4);
   }
}

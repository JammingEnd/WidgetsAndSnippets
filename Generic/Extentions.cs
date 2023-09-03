using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExtentionMethods
{
    public static class Extentions
    {
        //Bool for inbetween operations
        public static bool IsBetween(this bool B, float value, float min, float max)
        {
            return (value >= min && value <= max);
        }

        //check if a list contains nulls
        //
        public static IEnumerable<T> Safe<T>(this IEnumerable<T> source)
        {
            if (source == null)
            {
                yield break;
            }

            foreach (var item in source)
            {
                yield return item;
            }
        }
        //remove a word in a string
        public static string RemoveWord(this string original, string removedWord)
        {
            return original.Replace(removedWord, "").Replace("  ", " ");
        }
    }

}

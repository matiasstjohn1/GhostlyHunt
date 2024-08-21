using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyRandoms
{
    public static float Range(float min, float max)
    {
        return min + UnityEngine.Random.value * (max - min);
    }
    public static T Roulette<T>(Dictionary<T, int> items)
    {
        int total = 0;
        foreach (var item in items)
        {
            total += item.Value;
        }
        //var random = Random.Range(0, total);
        var random = Range(0, total);
        foreach (var item in items)
        {
            if (random <= item.Value)
            {
                return item.Key;
            }
            else
            {
                random -= item.Value;
            }
        }
        return default(T);
    }
}

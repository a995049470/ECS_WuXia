using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class ExtensionMethod 
{
    public static Vector3 GetColorRGB(this Color color)
    {
        return new Vector3(color.r, color.g, color.b);
    }
    public static void Foreach<T>(this T[] array, Action<T> action)
    {
        var len = array.Length;
        for (int i = 0; i < len; i++)
        {
            action?.Invoke(array[i]);
        }
    }

    public static void SetBit(ref this int n, int i, int v)
    {
        int t = i;
        if (v == 1)
        {
            n = n | (1 << t);
        }
        else if (v == 0)
        {
            n = n & ~(1 << t);
        }
    }

    public static int GetBit(ref this int n, int i)
    {
        int t = i;
        var res = (n & (1 << t)) >> t;
        return res;
    }
}

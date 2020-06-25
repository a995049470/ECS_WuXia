using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethod 
{
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

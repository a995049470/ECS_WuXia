using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;



[AttributeUsage(AttributeTargets.Class)]
public class BehaviorNodeAttribute : Attribute
{
    // public Type[] ConstructorTypes { get; private set; }
    public bool IsHasNext {get; private set;}
    public BehaviorNodeAttribute(bool isHasNext = true)
    {
        IsHasNext = isHasNext;
    }
}


[AttributeUsage(AttributeTargets.Field)]
public class SerializableNodeFieldAttribute : Attribute
{
    
    // public Type[] ConstructorTypes { get; private set; }
    public SerializableNodeFieldAttribute()
    {
        
    }
}


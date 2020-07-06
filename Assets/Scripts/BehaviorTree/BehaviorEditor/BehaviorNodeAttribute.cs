using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;



[AttributeUsage(AttributeTargets.Class)]
public class BehaviorNodeTag : Attribute
{
    // public Type[] ConstructorTypes { get; private set; }
    public BehaviorNodeTag()
    {
        
    }
}


[AttributeUsage(AttributeTargets.Field)]
public class SerializableNodeFieldTag : Attribute
{
    
    // public Type[] ConstructorTypes { get; private set; }
    public SerializableNodeFieldTag()
    {
        
    }
}


using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;



[AttributeUsage(AttributeTargets.Class)]
public class BehaviorNodeAttribute : Attribute
{
    // public Type[] ConstructorTypes { get; private set; }
    public string BaseType {get; private set;}
    public BehaviorNodeAttribute(string basetype = "BehaviorLinkXNode")
    {
        BaseType = basetype;
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


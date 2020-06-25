using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct C1 : IComponentData
{
    public FixedString32 name;
}

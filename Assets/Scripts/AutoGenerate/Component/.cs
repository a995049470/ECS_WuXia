using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using System;
using BT;

[Serializable]
[GenerateAuthoringComponent]
public struct  : IComponentData
{
	public UInt32 RootHandle;

}

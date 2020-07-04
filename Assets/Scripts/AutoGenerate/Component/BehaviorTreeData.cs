using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using System;
using BT;

[GenerateAuthoringComponent]
public struct BehaviorTreeData : IComponentData
{
	[UnityEngine.HideInInspector]
	public UInt32 RootHandle;
}

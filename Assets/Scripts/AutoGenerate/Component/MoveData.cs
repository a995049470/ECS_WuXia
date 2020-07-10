using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using System;
using BT;

[Serializable]
[GenerateAuthoringComponent]
public struct MoveData : IComponentData
{
	public float3 Target;
	public bool IsArrive;

}

using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using System;
using BT;

[Serializable]
[GenerateAuthoringComponent]
public struct RoundShapeData : IComponentData
{
	public int Radius;
	public int FollowState;

}

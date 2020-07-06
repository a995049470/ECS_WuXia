using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using System;
using BT;

[Serializable]
[GenerateAuthoringComponent]
public struct SectorShapeData : IComponentData
{
	public int Radius;
	public int Radian;
	public int FollowState;

}

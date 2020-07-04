using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using System;
using BT;

[GenerateAuthoringComponent]
public struct RectShapeData : IComponentData
{
	public int Height;
	public int Width;
	public int FollowState;

}

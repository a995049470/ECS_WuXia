using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct RoundShapeData : IComponentData
{
	public int Radius;
	public int FollowState;

}

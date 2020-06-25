using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct SkillTileData : IComponentData
{
	public int2 Offset;
	public int FollowState;

}

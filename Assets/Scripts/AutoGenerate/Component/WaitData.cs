using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using BT;

[GenerateAuthoringComponent]
public struct WaitData : IComponentData, IBehaviorData
{
	public float CurTime;
	public float TargetTime;
	public BTStatus Status { get; set; } 

}

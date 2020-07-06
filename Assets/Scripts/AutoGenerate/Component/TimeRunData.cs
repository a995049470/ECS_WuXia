using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using System;
using BT;

[Serializable]
[GenerateAuthoringComponent]
public struct TimeRunData : IComponentData, IBehaviorData
{
	public float CurTime;
	public float TargetTime;
	public BTStatus Status { get; set; } 

}

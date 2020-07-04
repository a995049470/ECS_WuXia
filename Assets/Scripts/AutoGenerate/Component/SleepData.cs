using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using System;
using BT;

[GenerateAuthoringComponent]
public struct SleepData : IComponentData, IBehaviorData
{
	public float Prob;
	public BTStatus Status { get; set; } 

}

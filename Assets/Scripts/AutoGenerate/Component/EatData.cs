using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using System;
using BT;

[Serializable]
[GenerateAuthoringComponent]
public struct EatData : IComponentData, IBehaviorData
{
	public float Prob;
	public BTStatus Status { get; set; } 

}

using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using System;
using BT;

[Serializable]
[GenerateAuthoringComponent]
public struct SuDuData : IComponentData
{
	public int Value;

}

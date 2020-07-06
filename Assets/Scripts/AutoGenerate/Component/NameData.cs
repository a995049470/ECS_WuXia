using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using System;
using BT;

[Serializable]
[GenerateAuthoringComponent]
public struct NameData : IComponentData
{
	public FixedString32 LastName;
	public FixedString32 FirstName;

}

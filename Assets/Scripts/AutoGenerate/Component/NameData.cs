using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using BT;

[GenerateAuthoringComponent]
public struct NameData : IComponentData
{
	public FixedString32 LastName;
	public FixedString32 FirstName;

}

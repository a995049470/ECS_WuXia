using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct InputData : IComponentData
{
	public int2 MousePos;
	public int KeyDownStates;
	public int KeyStates;
	public int InputMask;

}

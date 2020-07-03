using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class InputSystem : SystemBase
{
    private KeyCode[] m_keyCodes;
    private Camera m_mianCamera;
    //private EndSimulationEntityCommandBufferSystem m_system;
    protected override void OnCreate()
    {
        m_keyCodes = new KeyCode[]
        {
            KeyCode.Mouse0, 
            KeyCode.S, KeyCode.W,
            KeyCode.A, KeyCode.D,
            KeyCode.Alpha0, KeyCode.Alpha1,
            KeyCode.Alpha3, KeyCode.Alpha4
        };
        m_mianCamera = Camera.main;
    }


    protected override void OnUpdate()
    { 
       
        int keyDownStates = 0;
        int keyStates = 0;
        int v0 = 0;
        int v1 = 0;
        var mouseScreenPos = Input.mousePosition;
        var mouseWorldPos = m_mianCamera.ScreenToWorldPoint(mouseScreenPos);
        int2 mousePos = new int2(Mathf.RoundToInt(mouseWorldPos.x), Mathf.RoundToInt(mouseWorldPos.x));
        for (int i = 0; i < m_keyCodes.Length; i++)
        {
            v0 = Input.GetKeyDown(m_keyCodes[0]) ? 1 : 0;
            v1 = Input.GetKey(m_keyCodes[0]) ? 1 : 0;
            keyDownStates.SetBit(i, v0);
            keyStates.SetBit(i, v1);
        }
        
        Entities.ForEach((ref InputData inputData) => 
        {
            inputData.KeyDownStates = keyDownStates & inputData.InputMask;
            inputData.KeyStates = keyStates & inputData.InputMask;
        }).Schedule();
    }
}

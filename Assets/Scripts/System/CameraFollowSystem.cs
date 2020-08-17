using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

public class CameraFollowSystem : SystemBase
{
    private UnityEngine.Camera m_camera; 
    private float m_lerpSpd = 5;
    protected override void OnStartRunning()
    {
        m_camera = UnityEngine.Camera.main;
    }

    protected override void OnUpdate()
    {
        float deltaTime = Time.DeltaTime;
        Entities.ForEach((in FocusData fouces, in Translation trans) => 
        {
            float3 pos = m_camera.transform.position;
            pos.x = math.lerp(pos.x, trans.Value.x, deltaTime * m_lerpSpd);
            pos.y = math.lerp(pos.y, trans.Value.y, deltaTime * m_lerpSpd); 
            m_camera.transform.position = pos;
        }).WithoutBurst().Run();
       
    }
}

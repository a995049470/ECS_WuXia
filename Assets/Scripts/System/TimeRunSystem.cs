using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

public class TimeRunSystem : SystemBase
{
    protected override void OnUpdate()
    {
        float deltaTime = UnityEngine.Time.deltaTime;
        Entities.ForEach((ref TimeRunData data) => 
        {
            if(data.Status != BT.BTStatus.Running)
            {
                return;
            }
            data.CurTime += deltaTime;
            if(data.CurTime >= data.TargetTime)
            {
                data.Status = BT.BTStatus.Success;
            }
           
        }).Schedule();
    }
}

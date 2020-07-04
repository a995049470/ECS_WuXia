using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using BT;

public class TestSystem : SystemBase
{
    protected override void OnUpdate()
    {
        Entities.ForEach((Entity entity, ref EatData eatData) => 
        {
            if(eatData.Status != BTStatus.Running)
            {
                return;
            }
            eatData.Status = UnityEngine.Random.value < eatData.Prob ? 
                BTStatus.Success : BTStatus.Failure;
            UnityEngine.Debug.Log($"{entity} Eat: {eatData.Status} Prob : {eatData.Prob}");
        }).WithoutBurst().Run();

         Entities.ForEach((Entity entity, ref WashData washData) => 
        {
            if(washData.Status != BTStatus.Running)
            {
                return;
            }
            washData.Status = UnityEngine.Random.value < washData.Prob ? 
                BTStatus.Success : BTStatus.Failure;
            UnityEngine.Debug.Log($"{entity} Wash: {washData.Status} Prob : {washData.Prob}");
        }).WithoutBurst().Run();

         Entities.ForEach((Entity entity, ref SleepData sleepData) => 
        {
            if(sleepData.Status != BTStatus.Running)
            {
                return;
            }
            sleepData.Status = UnityEngine.Random.value < sleepData.Prob ? 
                BTStatus.Success : BTStatus.Failure;
            UnityEngine.Debug.Log($"{entity} Sleep: {sleepData.Status} Prob : {sleepData.Prob}");
        }).WithoutBurst().Run();
    }
}

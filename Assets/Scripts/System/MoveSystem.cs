using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

public class MoveSystem : SystemBase
{
    private const float m_lerpSpd = 10;
    private const float m_error = 0.1f;

    protected override void OnStartRunning()
    {
        var transGroup = GetComponentDataFromEntity<Translation>(true);
        var moveGroup = GetComponentDataFromEntity<MoveData>(false);
        var entities = GetEntityQuery(ComponentType.ReadOnly<Translation>(), ComponentType.ReadWrite<MoveData>()).
            ToEntityArray(Allocator.TempJob);
        var len = entities.Length;
        for (int i = 0; i < len; i++)
        {
            var entity = entities[i];
            if(transGroup.Exists(entity) &&
               moveGroup.Exists(entity))
            {
                var moveData = moveGroup[entity];
                var trans = transGroup[entity];
                moveData.Target = trans.Value;
                moveData.IsArrive = true;
                moveGroup[entity] = moveData;
            }
        }
        entities.Dispose();
    }

    protected override void OnUpdate()
    {
        float delatTime = UnityEngine.Time.deltaTime;
        Entities.ForEach((ref MoveData move, in InputData input)=>
        {
            
            int dirInput = input.KeyDownStates & ( (1 << 5 ) - 1);
            if(dirInput == 0)
            {
                return;
            }
            move.IsArrive = false;
            var dir = float3.zero;
            if(dirInput.GetBit(1) == 1)
            {
                dir.y = 1;
            }
            else if(dirInput.GetBit(2) == 1)
            {
                dir.y = -1;
            }
            else if(dirInput.GetBit(3) == 1)
            {
                dir.x = -1;
            }
            else if(dirInput.GetBit(4) == 1)
            {
                dir.x = 1;
            }
            move.Target += dir;
        }).Schedule();

        Entities.ForEach((ref MoveData move, ref Translation trans) => 
        {
            if(move.IsArrive)
            {
                return;
            }   
            var pos = math.lerp(trans.Value, move.Target, m_lerpSpd * delatTime);
            if(math.distance(pos, move.Target) < m_error)
            {
                pos = move.Target;
                move.IsArrive = true;
            }
            trans.Value = pos;
            
        }).Schedule();
    }
}

using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using BT;   

[UpdateInGroup(typeof(InitializationSystemGroup))]
public class BehaviorTreeSystem : SystemBase
{
    private EndSimulationEntityCommandBufferSystem m_system;

    protected override void OnStartRunning()
    {
        m_system = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
    }

    protected override void OnUpdate()
    {
        var buffer = m_system.CreateCommandBuffer();
        
        Entities.ForEach((Entity entity, ref BehaviorTreeData data) => 
        {
            if(data.RootHandle == 0)
            {
                data.RootHandle = BehaviorManager.Instance.TestCreateBT_1().GetHandle();
            }
            var handle = new Handle<RootNode>(data.RootHandle);
            var root = HandleManager<RootNode>.Instance.Get(handle);
            if(root != null)
            {
                root.Tick(entity, ref buffer);
                if(root.IsComplete())
                {
                    root.Terminate(entity, ref buffer);
                    HandleManager<RootNode>.Instance.Free(handle);
                }
            }
            else
            {
                buffer.RemoveComponent<BehaviorTreeData>(entity);
            }
        }).WithoutBurst().Run();
        m_system.AddJobHandleForProducer(this.Dependency);
    }
}

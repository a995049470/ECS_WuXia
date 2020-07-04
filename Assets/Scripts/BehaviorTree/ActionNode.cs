using Unity.Entities;

namespace BT
{
    public class ActionNode<T> : BehaviorNode where T : struct, IComponentData, IBehaviorData
    {
        private T m_data;
        public ActionNode(T data) : base()
        {
            m_data = data;
        }
        protected override void OnInitialize(Entity entity, ref EntityCommandBuffer buffer)
        {
            buffer.AddComponent(entity, m_data);
        }

        protected override void OnOnTerminate(Entity entity, ref EntityCommandBuffer buffer)
        {
            buffer.RemoveComponent<T>(entity);
        }

        protected override BTStatus Update(Entity entity, ref EntityCommandBuffer buffer)
        {
            var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            bool isHas = entityManager.HasComponent<T>(entity);
            if (isHas)
            {
                var data = entityManager.GetComponentData<T>(entity);
                m_btStatus = data.Status;
            }
            else
            {
                m_btStatus = BTStatus.Running;
            }
            return m_btStatus;
        }
    }

}


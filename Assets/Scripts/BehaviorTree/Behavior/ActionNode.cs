using Unity.Entities;

namespace BT
{
    

    public class ActionNode<T> : BehaviorNode where T : struct, IComponentData, IBehaviorData
    {
        private T m_data;
        private bool m_isBufferFrame;
        public ActionNode(T data) : base()
        {
            m_data = data;
        }
        protected override void OnInitialize(Entity entity, ref EntityCommandBuffer buffer)
        {
            
            var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            bool isHas = entityManager.HasComponent<T>(entity);
            if (isHas)
            {
                buffer.SetComponent(entity, m_data);
            }
            else
            {
                buffer.AddComponent(entity, m_data);
            }
            m_isBufferFrame = true;
        }

        internal override void OnTerminate(Entity entity, ref EntityCommandBuffer buffer)
        {      
            buffer.RemoveComponent<T>(entity);
        }

        protected override BTStatus Update(Entity entity, ref EntityCommandBuffer buffer)
        {
            if (m_isBufferFrame)
            {
                m_btStatus = BTStatus.Running;
                m_isBufferFrame = false;
            }
            else
            {
                var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
                var data = entityManager.GetComponentData<T>(entity);
                m_btStatus = data.Status;
            }
            return m_btStatus;
        }
    }

}


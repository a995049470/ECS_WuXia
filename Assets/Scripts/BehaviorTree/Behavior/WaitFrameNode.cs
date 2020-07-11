using Unity.Entities;

namespace BT
{
    [BehaviorNode(false)]
    public class WaitFrameNode : BehaviorNode
    {
        public WaitFrameNode(int waitFrame)
        {
            m_waitFrame = waitFrame;
        }
        [SerializableNodeField]
        private int m_waitFrame;
        private int m_frameTimer;
        protected override void OnInitialize(Entity entity, ref EntityCommandBuffer buffer)
        {
            m_frameTimer = 0;
            m_btStatus = BTStatus.Running;
        }

        protected override BTStatus Update(Entity entity, ref EntityCommandBuffer buffer)
        {
            
            m_frameTimer ++;
            if(m_frameTimer == m_waitFrame)
            {
                m_btStatus = BTStatus.Success;
            }
            return m_btStatus;
        }
    }

}

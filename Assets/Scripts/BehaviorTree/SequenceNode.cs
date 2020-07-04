using Unity.Entities;

namespace BT
{
    //次序节点
    public class SequenceNode : BehaviorNode
    {
        public SequenceNode(BehaviorNode[] childs) : base(childs)
        {
            
        }
        
        protected override void OnInitialize(Entity entity, ref EntityCommandBuffer buffer)
        {
            
        }

        protected override void OnOnTerminate(Entity entity, ref EntityCommandBuffer buffer)
        {

        }

        protected override BTStatus Update(Entity entity, ref EntityCommandBuffer buffer)
        {
            for (int i = 0; i < m_childs.Length; i++)
            {
                var node = m_childs[i];
                m_btStatus = node.Tick(entity, ref buffer);
                if (m_btStatus == BTStatus.Failure || m_btStatus == BTStatus.Running)
                {
                    break;
                }
                else if (m_btStatus == BTStatus.Success)
                {
                    continue;
                }
            #if UNITY_EDITOR
                node.InvaildCheck();
            #endif
            }
            return m_btStatus;
        }
    }




}


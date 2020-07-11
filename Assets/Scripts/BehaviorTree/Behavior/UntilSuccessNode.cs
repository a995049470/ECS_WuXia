using Unity.Entities;

namespace BT
{
    //当子节点返回Success 时才会返回 Success
    //TODO : 如果子节点能够马上给出结果, 可能会考虑实现一帧跑多次
    [BehaviorNodeAttribute]
    public class UntilSuccessNode : BehaviorNode
    {
        [SerializableNodeFieldAttribute]
        private int m_goal;
        private int m_current;

        public UntilSuccessNode(int goal, params BehaviorNode[] childs) : base(childs)
        {
            m_goal = goal;
        }

        protected override void OnInitialize(Entity entity, ref EntityCommandBuffer buffer)
        {
            m_current = 0;
        }

        protected override BTStatus Update(Entity entity, ref EntityCommandBuffer buffer)
        {
        #if UNITY_EDITOR
            if (m_childs?.Length != 1)
            {
                throw new System.Exception($"{this.GetType()} 子节点数目不为1");
            }
        #endif
            var node = m_childs[0];
            if(m_goal == m_current)
            {
                m_btStatus = BTStatus.Failure;
            }
            else
            {
                m_btStatus = node.Tick(entity, ref buffer);
                if(m_btStatus == BTStatus.Failure)
                {
                    node.Restart();
                    m_btStatus = BTStatus.Running;
                    m_current += m_goal > 0 ? 1 : 0;
                }
            }
            return m_btStatus;
        }
    }




}


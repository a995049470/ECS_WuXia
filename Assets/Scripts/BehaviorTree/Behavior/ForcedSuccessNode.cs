using Unity.Entities;

namespace BT
{
    [BehaviorNodeAttribute]
    public class ForcedSuccessNode : BehaviorNode
    {
        public ForcedSuccessNode(params BehaviorNode[] childs) : base(childs)
        {

        }

        protected override void OnInitialize(Entity entity, ref EntityCommandBuffer buffer)
        {
            
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
            m_btStatus = node.Tick(entity, ref buffer);
            if (m_btStatus == BTStatus.Failure)
            {
                m_btStatus = BTStatus.Success;
            }
        #if UNITY_EDITOR
            node.InvaildCheck();
        #endif
            return m_btStatus;
        }
    }
}


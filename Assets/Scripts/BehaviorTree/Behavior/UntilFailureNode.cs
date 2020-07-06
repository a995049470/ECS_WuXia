using Unity.Entities;
using System.Collections;
using System.Collections.Generic;
using System;
namespace BT
{   
    //当子节点返回Failure 时才会返回 Success
    [BehaviorNodeTag]
    public class UntilFailureNode : BehaviorNode
    {
        [SerializableNodeFieldTag]
        private int m_goal;
        private int m_current;

        public UntilFailureNode(int goal,params BehaviorNode[] childs) : base(childs)
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
                if(m_btStatus == BTStatus.Success)
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


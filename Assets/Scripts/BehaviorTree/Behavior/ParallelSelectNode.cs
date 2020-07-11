using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace BT
{
    [BehaviorNodeAttribute]
    public class ParallelSelectNode : BehaviorNode
    {
        public ParallelSelectNode(params BehaviorNode[] childs) : base(childs)
        {

        }
        protected override void OnInitialize(Entity entity, ref EntityCommandBuffer buffer)
        {
            
        }

        protected override BTStatus Update(Entity entity, ref EntityCommandBuffer buffer)
        {
            m_btStatus = BTStatus.Failure;
            for (int i = 0; i < m_childs.Length; i++)
            {
                var node = m_childs[i];
                var status = node.Tick(entity, ref buffer);
                if(status == BTStatus.Running)
                {
                    m_btStatus = status;
                }
                else if(status == BTStatus.Success && 
                        m_btStatus != BTStatus.Running)
                {
                    m_btStatus = status;
                }
                #if UNITY_EDITOR
                    node.InvaildCheck();
                #endif
            }
            return m_btStatus;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace BT
{
    //节点并行执行  直到有一个失败 节点返回失败
    [BehaviorNodeAttribute]
    public class ParallelSequenceNode : BehaviorNode
    {
        public ParallelSequenceNode(params BehaviorNode[] childs) : base(childs)
        {

        }
        protected override void OnInitialize(Entity entity, ref EntityCommandBuffer buffer)
        {
            
        }

        protected override BTStatus Update(Entity entity, ref EntityCommandBuffer buffer)
        {
            m_btStatus = BTStatus.Success;
            for (int i = 0; i < m_childs.Length; i++)
            {
                var node = m_childs[i];
                var status = node.Tick(entity, ref buffer);
                if(status == BTStatus.Running)
                {
                    m_btStatus = status;
                }
                else if(status == BTStatus.Failure /*&& m_btStatus != BTStatus.Running*/)
                {
                    m_btStatus = status;
                    break;
                }
                #if UNITY_EDITOR
                    node.InvaildCheck();
                #endif
            }
            return m_btStatus;
        }
    }

}

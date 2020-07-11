using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace BT
{
    public class WaitTimeNode : BehaviorNode
    {
        private float m_waitTime;
        private float m_timer;
        protected override void OnInitialize(Entity entity, ref EntityCommandBuffer buffer)
        {
            
        }

        protected override BTStatus Update(Entity entity, ref EntityCommandBuffer buffer)
        {
            
            return m_btStatus;
        }
    }

}

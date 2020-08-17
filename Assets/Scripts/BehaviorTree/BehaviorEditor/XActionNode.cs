using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using Unity.Entities;

namespace BT
{
    public class XActionNode<T> : XBehaviorNode where T : struct, IComponentData, IBehaviorData
    {
        [Input(backingValue = ShowBackingValue.Never)] public byte Enter = 0;
        public T Data;
        public override BehaviorNode GetBehaviorNode()
        {
            Data.Status = BTStatus.Running;
            return new ActionNode<T>(Data);
        }
    }
}


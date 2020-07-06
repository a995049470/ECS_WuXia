using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using Unity.Entities;

namespace BT
{
    public class ActionXNode<T> : BehaviorXNode where T : struct, IComponentData, IBehaviorData
    {
        public T Data;
        public override BehaviorNode GetBehaviorNode()
        {
            Data.Status = BTStatus.Running;
            return new ActionNode<T>(Data);
        }
    }
}


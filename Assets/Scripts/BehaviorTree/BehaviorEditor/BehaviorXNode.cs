using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace BT
{
    public abstract class BehaviorXNode : Node
    {
        [Input(backingValue = ShowBackingValue.Never)] public byte Enter = 0;

        public abstract BehaviorNode GetBehaviorNode();
        public override object GetValue(NodePort port) 
        {
            
            return null;
        }
    }
    public abstract class BehaviorLinkXNode : BehaviorXNode
    {
        [Output(backingValue = ShowBackingValue.Never)] public byte Next = 0;
        protected BehaviorNode[] GetChilds()
        {
            var nodes = GetOutputPort("Next").GetConnections();
            var count = nodes?.Count ?? 0;
            var childs = new BehaviorNode[count];
            for (int i = 0; i < count; i++)
            {
                var xnode = nodes[i].node as BehaviorXNode;
                childs[i] = xnode?.GetBehaviorNode();
            }
            return childs;
        }
    }
}





using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class XRootNode : XBehaviorNode
    {
        [Output(backingValue = ShowBackingValue.Never)] public byte Next = 0;
        public override BehaviorNode GetBehaviorNode()
        {
            return new RootNode(GetChilds());
        }
        protected BehaviorNode[] GetChilds()
        {
            var nodes = GetOutputPort("Next").GetConnections();
            var count = nodes?.Count ?? 0;
            var childs = new BehaviorNode[count];
            for (int i = 0; i < count; i++)
            {
                var xnode = nodes[i].node as XBehaviorNode;
                childs[i] = xnode?.GetBehaviorNode();
            }
            return childs;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace BT
{
    [CreateAssetMenu]
    public class BehaviorGraph : NodeGraph
    {
        public RootNode GetRootNode()
        {
            RootNode root = null;
            if(nodes?.Count > 0)
            {
                var behaviorNode = (nodes[0] as BehaviorXNode)?.GetBehaviorNode() as BehaviorNode;
                root = behaviorNode != null ? new RootNode(behaviorNode) : null;
            }
            return root;
        }
    }

}

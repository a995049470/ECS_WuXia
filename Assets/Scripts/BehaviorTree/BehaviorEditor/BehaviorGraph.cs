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
            var count = nodes?.Count;
            for (int i = 0; i < count; i++)
            {
                var node = nodes[i] as XRootNode;
                if(node != null)
                {
                    root = node.GetBehaviorNode() as RootNode;
                    break;
                }
            }
            return root;
        }
    }

}

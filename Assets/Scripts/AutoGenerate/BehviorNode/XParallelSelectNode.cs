using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class XParallelSelectNode : BehaviorLinkXNode
    {

        
        public override BehaviorNode GetBehaviorNode()
        {
            return new ParallelSelectNode(GetChilds());
        }
    }
}
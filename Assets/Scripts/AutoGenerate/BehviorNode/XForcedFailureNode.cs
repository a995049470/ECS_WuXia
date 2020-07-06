using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class XForcedFailureNode : BehaviorLinkXNode
    {

        
        public override BehaviorNode GetBehaviorNode()
        {
            return new ForcedFailureNode(GetChilds());
        }
    }
}
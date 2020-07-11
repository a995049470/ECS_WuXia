using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class XForcedFailureNode : BT.XBehaviorLinkNode
    {

        
        public override BehaviorNode GetBehaviorNode()
        {
            return new ForcedFailureNode(GetChilds());
        }
    }
}
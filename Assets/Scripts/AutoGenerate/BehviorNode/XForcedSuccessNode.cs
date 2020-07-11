using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class XForcedSuccessNode : BT.XBehaviorLinkNode
    {

        
        public override BehaviorNode GetBehaviorNode()
        {
            return new ForcedSuccessNode(GetChilds());
        }
    }
}
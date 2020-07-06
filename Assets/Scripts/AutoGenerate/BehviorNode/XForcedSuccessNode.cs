using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class XForcedSuccessNode : BehaviorLinkXNode
    {

        
        public override BehaviorNode GetBehaviorNode()
        {
            return new ForcedSuccessNode(GetChilds());
        }
    }
}
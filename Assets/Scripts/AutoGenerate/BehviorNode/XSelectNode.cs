using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class XSelectNode : BehaviorLinkXNode
    {

        
        public override BehaviorNode GetBehaviorNode()
        {
            return new SelectNode(GetChilds());
        }
    }
}
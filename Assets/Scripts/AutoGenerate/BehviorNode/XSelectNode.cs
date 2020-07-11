using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class XSelectNode : BT.XBehaviorLinkNode
    {

        
        public override BehaviorNode GetBehaviorNode()
        {
            return new SelectNode(GetChilds());
        }
    }
}
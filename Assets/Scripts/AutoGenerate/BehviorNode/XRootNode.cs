using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class XRootNode : BT.XBehaviorLinkNode
    {

        
        public override BehaviorNode GetBehaviorNode()
        {
            return new RootNode(GetChilds());
        }
    }
}
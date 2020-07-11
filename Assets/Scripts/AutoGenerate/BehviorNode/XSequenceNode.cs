using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class XSequenceNode : BT.XBehaviorLinkNode
    {

        
        public override BehaviorNode GetBehaviorNode()
        {
            return new SequenceNode(GetChilds());
        }
    }
}
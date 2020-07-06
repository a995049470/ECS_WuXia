using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class XSequenceNode : BehaviorLinkXNode
    {

        
        public override BehaviorNode GetBehaviorNode()
        {
            return new SequenceNode(GetChilds());
        }
    }
}
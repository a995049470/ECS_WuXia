using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class XParallelSequenceNode : BehaviorLinkXNode
    {

        
        public override BehaviorNode GetBehaviorNode()
        {
            return new ParallelSequenceNode(GetChilds());
        }
    }
}
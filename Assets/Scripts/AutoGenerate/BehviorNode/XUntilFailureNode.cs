using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class XUntilFailureNode : BT.XBehaviorLinkNode
    {
		public System.Int32 Goal;

        
        public override BehaviorNode GetBehaviorNode()
        {
            return new UntilFailureNode(Goal, GetChilds());
        }
    }
}
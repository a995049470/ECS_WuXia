using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class XUntilSuccessNode : BehaviorLinkXNode
    {
		public System.Int32 Goal;

        
        public override BehaviorNode GetBehaviorNode()
        {
            return new UntilSuccessNode(Goal, GetChilds());
        }
    }
}
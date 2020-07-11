using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class XWaitFrameNode : BT.XBehaviorNode
    {
		public System.Int32 WaitFrame;

        
        public override BehaviorNode GetBehaviorNode()
        {
            return new WaitFrameNode(WaitFrame);
        }
    }
}
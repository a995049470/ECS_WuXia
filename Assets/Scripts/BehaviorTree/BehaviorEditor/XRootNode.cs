using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class XRootNode : XBehaviorLinkNode
    {
        //[Output(backingValue = ShowBackingValue.Never)] public byte Next = 0;
        public override BehaviorNode GetBehaviorNode()
        {
            return new RootNode(GetChilds());
        }
       
    }
}
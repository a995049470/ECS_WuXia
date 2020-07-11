using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace BT
{
    public abstract class XBehaviorNode : Node
    {
        [Input(backingValue = ShowBackingValue.Never)] public byte Enter = 0;

        public abstract BehaviorNode GetBehaviorNode();
        public override object GetValue(NodePort port) 
        {
            
            return null;
        }
    }
}





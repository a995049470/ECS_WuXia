using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace BT
{
    public abstract class XBehaviorNode : Node
    {
        public abstract BehaviorNode GetBehaviorNode();
        public override object GetValue(NodePort port) 
        {
            
            return null;
        }
    }
}





using Unity.Entities;

namespace BT
{
    //等待节点
    public class WaitNode : ActionNode<WaitData>
    {
        public WaitNode(WaitData data) : base(data)
        {

        }
    }
}


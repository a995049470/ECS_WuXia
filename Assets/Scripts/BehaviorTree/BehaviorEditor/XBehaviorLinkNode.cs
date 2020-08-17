namespace BT
{
    public abstract class XBehaviorLinkNode : XBehaviorNode
    {
        [Output(backingValue = ShowBackingValue.Never)] public byte Next = 0;
        protected BehaviorNode[] GetChilds()
        {
            var nodes = GetOutputPort("Next").GetConnectionsOrderly();
            var count = nodes?.Count ?? 0;
            var childs = new BehaviorNode[count];
            for (int i = 0; i < count; i++)
            {
                var xnode = nodes[i].node as XBehaviorNode;
                childs[i] = xnode?.GetBehaviorNode();
            }
            return childs;
        }
    }
}





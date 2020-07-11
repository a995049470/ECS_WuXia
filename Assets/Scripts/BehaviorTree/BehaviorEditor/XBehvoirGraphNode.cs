namespace BT
{
    public class XBehvoirGraphNode : XBehaviorNode
    {
        public BehaviorGraph Graph;
        public override BehaviorNode GetBehaviorNode()
        {
           return Graph.GetRootNode();
        }
    }
}


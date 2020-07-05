using Unity.Entities;

namespace BT
{
    //等待节点
    //等待完成之后一定返回true
    public class WaitNode : ActionNode<TimeRunData>
    {
        public WaitNode(float waitTime) : base(new TimeRunData()
        {
            CurTime = 0,
            TargetTime = waitTime,
            Status = BTStatus.Running, 
        })
        {

        }
    }
}


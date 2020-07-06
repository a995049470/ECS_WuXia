using System.Collections.Generic;
using Unity.Entities;

namespace BT
{
    //[BehaviorNodeInfo()]
    //行为树的根节点 也可以作为其他节点子节点
    public class RootNode : BehaviorNode, IHandle
    {
        public RootNode(params BehaviorNode[] childs) : base(childs)
        {

        }
        public void OnFree()
        {
           
        }

        //递归终止字节点 
        public void Terminate(Entity entity, ref EntityCommandBuffer buffer)
        {
            Stack<BehaviorNode> s = new Stack<BehaviorNode>();
            s.Push(this);
            while (s.Count > 0)
            {
                var node = s.Pop();
                if(node.IsComplete())
                {
                    node.OnTerminate(entity, ref buffer);
                    //销毁
                    var childs = node.GetChilds();
                    for (int i = 0; i < childs.Length; i++)
                    {
                        s.Push(childs[i]);
                    }
                }
            }
        }


        protected override void OnInitialize(Entity entity, ref EntityCommandBuffer buffer)
        {
            
        }


        protected override BTStatus Update(Entity entity, ref EntityCommandBuffer buffer)
        {
        #if UNITY_EDITOR
            if (m_childs?.Length != 1)
            {
                throw new System.Exception($"{this.GetType()} 子节点数目不为1");
            }
        #endif
            var node = m_childs[0];
            m_btStatus = node.Tick(entity, ref buffer);
        #if UNITY_EDITOR
            node.InvaildCheck();
        #endif
            return m_btStatus;
        }
    }

}


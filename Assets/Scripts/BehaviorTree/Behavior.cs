using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace BT
{
    public enum BTStatus
    {
        Invalid,   //初始状态
        Success,   //成功
        Failure,   //失败
        Running,   //运行
        //Aborted,   //终止
    }

    public class BehaviorTree
    {
        private BehaviorNode m_root;
    }

    //所有行为的父节点
    public abstract class BehaviorNode
    {
        public BehaviorNode()
        {
            m_btStatus = BTStatus.Invalid;
        }

        public BTStatus Tick(Entity entity, ref EntityCommandBuffer buffer)
        {
            if(m_btStatus == BTStatus.Invalid)
            {
                OnInitialize(entity, ref buffer);
            }
            var oldStatus = m_btStatus;
            m_btStatus = Update(entity);

            if(m_btStatus != BTStatus.Running && oldStatus == BTStatus.Running)
            {
                OnOnTerminate(entity, ref buffer);
            }
            return m_btStatus;
        }

        public BehaviorNode[] GetChilds() { return m_childs ?? new BehaviorNode[0]; } 
        public BTStatus GetBTStatus() { return m_btStatus; }

        //尝试重启自身
        public bool TryRestart()
        {
            bool isRestart = false;
            if(m_btStatus == BTStatus.Failure || m_btStatus == BTStatus.Success)
            {
                m_btStatus = BTStatus.Invalid;
                isRestart = true;
            }
            return isRestart;
        }

        //迭代重启所有子节点
        public void IterateRestart()
        {
            Stack<BehaviorNode> s = new Stack<BehaviorNode>();
            s.Push(this);
            //DFS
            while (s.Count > 0)
            {
                var node = s.Pop();
                var status = node.GetBTStatus();
                if(node.TryRestart())
                {
                    var childs = node.GetChilds();
                    for (int i = 0; i < childs.Length; i++)
                    {
                        s.Push(childs[i]);
                    }
                }
            }
        }

        protected abstract void OnInitialize(Entity entity, ref EntityCommandBuffer buffer);
        protected abstract BTStatus Update(Entity entity);
        protected abstract void OnOnTerminate(Entity entity, ref EntityCommandBuffer buffer);
        protected BTStatus m_btStatus;
        private BehaviorNode[] m_childs;
    }

    //通过动作节点
    

    //选择节点
    public class SelectNode : BehaviorNode
    {
        protected override void OnInitialize(Entity entity, ref EntityCommandBuffer buffer)
        {
            
        }

        protected override void OnOnTerminate(Entity entity, ref EntityCommandBuffer buffer)
        {
           
        }

        protected override BTStatus Update(Entity entity)
        {
            return m_btStatus;
        }
    }

    //次序节点
    public class SequenceNode : BehaviorNode
    {
        protected override void OnInitialize(Entity entity, ref EntityCommandBuffer buffer)
        {
            
        }

        protected override void OnOnTerminate(Entity entity, ref EntityCommandBuffer buffer)
        {
            
        }

        protected override BTStatus Update(Entity entity)
        {
            return m_btStatus;
        }
    }

    //等待节点
    // public class WaitNode : BehaviorNode
    // {
    //     private float m_waitTime;
    //     protected override void OnInitialize(Entity entity, ref EntityCommandBuffer buffer)
    //     {
    //         buffer.AddComponent(entity, new WaitData()
    //         {
    //             CurTime = 0,
    //             TargetTime = m_waitTime,
    //             Status = BTStatus.Running 
    //         });
    //     }

    //     protected override void OnOnTerminate(Entity entity, ref EntityCommandBuffer buffer)
    //     {
    //         buffer.RemoveComponent<WaitData>(entity);   
    //     }

    //     protected override BTStatus Update(Entity entity)
    //     {
    //         if(m_btStatus != BTStatus.Success && m_btStatus != BTStatus.Failure)
    //         {
    //             var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
    //             bool isHas = entityManager.HasComponent<WaitData>(entity);
    //             if(isHas)
    //             {
    //                 var waitData = entityManager.GetComponentData<WaitData>(entity);
    //                 m_btStatus = waitData.Status;
    //             }
    //             else
    //             {
    //                 m_btStatus = BTStatus.Running;
    //             }
    //         }
            
    //         return m_btStatus;
    //     }
    // }




}


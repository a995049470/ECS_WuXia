using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace BT
{
    public enum BTStatus
    {
        Invalid,   //无效
        Success,   //成功
        Failure,   //失败
        Running,   //运行
        //Aborted,   //终止
    }


    //所有行为的父节点
    public abstract class BehaviorNode
    {
        public  BehaviorNode()
        {
            m_btStatus = BTStatus.Invalid;
            m_childs = m_emptyNodes;
        }

        public BehaviorNode(BehaviorNode[] childs) 
        {
            m_btStatus = BTStatus.Invalid;
            m_childs = childs;
        }

        protected static BehaviorNode[] m_emptyNodes = new BehaviorNode[0];

        public BTStatus Tick(Entity entity, ref EntityCommandBuffer buffer)
        {
            if (!IsComplete())
            {
                if (m_btStatus == BTStatus.Invalid)
                {
                    OnInitialize(entity, ref buffer);
                }             
                m_btStatus = Update(entity, ref buffer);
            #if UNITY_EDITOR
                // if(IsComplete())
                // {
                //     UnityEngine.Debug.Log($"{this.GetType()} {m_btStatus}");
                // }
            #endif
            }
            return m_btStatus;
        }

        public BehaviorNode[] GetChilds() { return m_childs ?? m_emptyNodes; }
        public BTStatus GetBTStatus() { return m_btStatus; }

        //尝试重启自身
        private bool TryRestart()
        {
            bool isRestart = false;
            if (IsComplete())
            {
                m_btStatus = BTStatus.Invalid;
                isRestart = true;
            }
            return isRestart;
        }


        //迭代重启自己及所有子节点
        public void Restart()
        {
            Stack<BehaviorNode> s = new Stack<BehaviorNode>();
            s.Push(this);
            //DFS
            while (s.Count > 0)
            {
                var node = s.Pop();
                if (node.TryRestart())
                {
                    var childs = node.GetChilds();
                    for (int i = 0; i < childs.Length; i++)
                    {
                        s.Push(childs[i]);
                    }
                }
            }
        }

        internal virtual void OnTerminate(Entity entity, ref EntityCommandBuffer buffer)
        {
            
        }

        public bool IsComplete()
        {
            return m_btStatus == BTStatus.Failure || m_btStatus == BTStatus.Success;
        }

        public bool IsInvalid()
        {
            return m_btStatus == BTStatus.Invalid;
        }

    #if UNITY_EDITOR
        public void InvaildCheck()
        {
            if (IsInvalid())
            {
                throw new System.Exception($"{this.GetType()} 回调状态出错");
            }
        }
    #endif
        protected abstract void OnInitialize(Entity entity, ref EntityCommandBuffer buffer);
        protected abstract BTStatus Update(Entity entity, ref EntityCommandBuffer buffer);
        
        
        protected BTStatus m_btStatus;
        protected BehaviorNode[] m_childs;
    }




}


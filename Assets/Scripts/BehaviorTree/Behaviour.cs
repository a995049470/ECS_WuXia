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
        private Behaviour m_root;
        
    }

    //所有行为的父节点
    public abstract class Behavior
    {
        protected abstract BTStatus Tick(Entity entity, ref EntityCommandBuffer buffer);
        protected abstract void OnInitialize(Entity entity, ref EntityCommandBuffer buffer);
        protected abstract void Update(Entity entity, ref EntityCommandBuffer buffer);
        protected abstract void OnOnTerminate(Entity entity, ref EntityCommandBuffer buffer);
        protected BTStatus m_btStatus;

        public BTStatus GetBTStatus() { return m_btStatus; }
    }

    // public class WaitBehavior : Behavior
    // {
        
    // }

}


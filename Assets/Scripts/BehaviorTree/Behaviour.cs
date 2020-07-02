using System.Collections;
using System.Collections.Generic;
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
        protected abstract BTStatus Tick(Agent agent);
        protected abstract void OnInitialize();
        protected abstract BTStatus Update();
        protected abstract void OnOnTerminate(BTStatus status);

        public BTStatus GetBTStatus() { return m_btStatus; }
        private BTStatus m_btStatus;
    }



}


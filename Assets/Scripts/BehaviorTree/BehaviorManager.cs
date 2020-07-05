﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class BehaviorManager : Single<BehaviorManager>
    {
        public Handle<RootNode> TestCreateBT_1()
        {
            Handle<RootNode> handle;
            RootNode root = new RootNode(new BehaviorNode[]
            {
                new SequenceNode(new BehaviorNode[]
                {
                    new SelectNode(new BehaviorNode[]
                    {
                        new ActionNode<EatData>(new EatData(){ Prob = Random.value, Status = BTStatus.Running }),
                        new ActionNode<EatData>(new EatData(){ Prob = Random.value, Status = BTStatus.Running }),
                        new ActionNode<EatData>(new EatData(){ Prob = Random.value, Status = BTStatus.Running }),
                    }),
                    new SelectNode(new BehaviorNode[]
                    {
                        new ActionNode<WashData>(new WashData(){ Prob = Random.value, Status = BTStatus.Running }),
                        new ActionNode<WashData>(new WashData(){ Prob = Random.value, Status = BTStatus.Running }),
                        new ActionNode<WashData>(new WashData(){ Prob = Random.value, Status = BTStatus.Running }),
                    }),
                    
                    new SelectNode(new BehaviorNode[]
                    {
                        new ActionNode<SleepData>(new SleepData(){ Prob = Random.value, Status = BTStatus.Running }),
                        new ActionNode<SleepData>(new SleepData(){ Prob = Random.value, Status = BTStatus.Running }),
                        new ActionNode<SleepData>(new SleepData(){ Prob = Random.value, Status = BTStatus.Running }),
                    }),
                })
            });
            handle = HandleManager<RootNode>.Instance.Put(root);
            return handle;
        }
        public Handle<RootNode> TestCreateBT_2()
        {
            Handle<RootNode> handle;
            RootNode root = new RootNode(new BehaviorNode[]
            {
                new SequenceNode(new BehaviorNode[]
                {
                    new ActionNode<EatData>(new EatData(){ Prob = Random.value, Status = BTStatus.Running }),
                    new ActionNode<WashData>(new WashData(){ Prob = Random.value, Status = BTStatus.Running }),
                    new ActionNode<SleepData>(new SleepData(){ Prob = Random.value, Status = BTStatus.Running }),
                })
            });
            handle = HandleManager<RootNode>.Instance.Put(root);
            return handle;
        }
    }

}


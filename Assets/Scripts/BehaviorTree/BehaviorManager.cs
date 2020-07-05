using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class BehaviorManager : Single<BehaviorManager>
    {
        public Handle<RootNode> TestCreateBT_1()
        {
            Handle<RootNode> handle;
            RootNode root = new RootNode(new SequenceNode(new BehaviorNode[]
                {
                    new SelectNode(new BehaviorNode[]
                    {
                        new ActionNode<EatData>(new EatData(){ Prob = Random.value, Status = BTStatus.Running }),
                        new ForcedFailureNode(new WaitNode(1)), 
                        new ActionNode<EatData>(new EatData(){ Prob = Random.value, Status = BTStatus.Running }),
                        new ForcedFailureNode(new WaitNode(1)), 
                        new ActionNode<EatData>(new EatData(){ Prob = Random.value, Status = BTStatus.Running }),
                    }),
                    new WaitNode(1),
                    new SelectNode(new BehaviorNode[]
                    {
                        new ActionNode<WashData>(new WashData(){ Prob = Random.value, Status = BTStatus.Running }),
                        new ForcedFailureNode(new WaitNode(1)), 
                        new ActionNode<WashData>(new WashData(){ Prob = Random.value, Status = BTStatus.Running }),
                        new ForcedFailureNode(new WaitNode(1)), 
                        new ActionNode<WashData>(new WashData(){ Prob = Random.value, Status = BTStatus.Running }),
                    }),
                    new WaitNode(1),
                    new SelectNode(new BehaviorNode[]
                    {
                        new ActionNode<SleepData>(new SleepData(){ Prob = Random.value, Status = BTStatus.Running }),
                        new ForcedFailureNode(new WaitNode(1)), 
                        new ActionNode<SleepData>(new SleepData(){ Prob = Random.value, Status = BTStatus.Running }),
                        new ForcedFailureNode(new WaitNode(1)),
                        new ActionNode<SleepData>(new SleepData(){ Prob = Random.value, Status = BTStatus.Running }),
                    }),
                })
            );
            handle = HandleManager<RootNode>.Instance.Put(root);
            return handle;
        }
        public Handle<RootNode> TestCreateBT_2()
        {
            Handle<RootNode> handle;
            RootNode root = new RootNode(new SequenceNode(new BehaviorNode[]
                {
                    new UntilSuccessNode(10, new SequenceNode(new BehaviorNode[]
                    {
                        new WaitNode(1),
                        new ActionNode<EatData>(new EatData(){ Prob = Random.value, Status = BTStatus.Running }),
                    })),

                    new UntilSuccessNode(10, new SequenceNode(new BehaviorNode[]
                    {
                        new WaitNode(1),
                        new ActionNode<WashData>(new WashData(){ Prob = Random.value, Status = BTStatus.Running }),
                    })),

                    new UntilSuccessNode(10, new SequenceNode(new BehaviorNode[]
                    {
                        new WaitNode(1),
                        new ActionNode<SleepData>(new SleepData(){ Prob = Random.value, Status = BTStatus.Running }),
                    })),
                })
            );
            handle = HandleManager<RootNode>.Instance.Put(root);
            return handle;
        }
    }

}


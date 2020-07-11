using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class BehaviorManager : Single<BehaviorManager>
    {
         
      
        public Handle<RootNode> TestCreateBT_2()
        {
            // Handle<RootNode> handle;
            // RootNode root = new RootNode(new SequenceNode(new BehaviorNode[]
            //     {
            //         new UntilSuccessNode(10, new SequenceNode(new BehaviorNode[]
            //         {
            //             new WaitNode(1),
            //             new ActionNode<EatData>(new EatData(){ Prob = Random.value, Status = BTStatus.Running }),
            //         })),

            //         new UntilSuccessNode(10, new SequenceNode(new BehaviorNode[]
            //         {
            //             new WaitNode(1),
            //             new ActionNode<WashData>(new WashData(){ Prob = Random.value, Status = BTStatus.Running }),
            //         })),

            //         new UntilSuccessNode(10, new SequenceNode(new BehaviorNode[]
            //         {
            //             new WaitNode(1),
            //             new ActionNode<SleepData>(new SleepData(){ Prob = Random.value, Status = BTStatus.Running }),
            //         })),
            //     })
            // );
            RootNode root = Resources.Load<BehaviorGraph>("BehaviorTrees/TestTree").GetRootNode();
            var handle = HandleManager<RootNode>.Instance.Put(root);
            return handle;
        }
    }

}


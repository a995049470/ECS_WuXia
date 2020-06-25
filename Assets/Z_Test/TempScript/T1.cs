using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.Collections;
using UnityEngine;

public class T1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FixedString32 str = new FixedString32("火1A");
        FixedListInt32 list = new FixedListInt32();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            Event e = Event.current;
            
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.Collections;
using UnityEngine;

public unsafe class T1 : MonoBehaviour
{
    // Start is called before the first frame update
    int n = 10;
    public int count;
    void Start()
    {
        // Debug.Log(n++);
        // Debug.Log(++n);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            Event e = Event.current;
            
        }
        
    }

    private static System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
    public static void StartSW()
    {
        sw.Restart();
    }
    public static void EndSW(string tag)
    {
        sw.Stop();
        var t = sw.ElapsedMilliseconds;
        if(t < 1)
        {
            Debug.Log($"<color=#ff0000ff>{tag}  time: < 1ms</color>");
            return;
        }
        Debug.Log($"<color=#ff0000ff>{tag}  time: {t}ms</color>");
    }
}

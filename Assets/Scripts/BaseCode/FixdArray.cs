using System;
using System.Runtime.InteropServices;
public unsafe struct FixedArray<T> where T : unmanaged
{
    private T* m_ptr;
    private int m_length;
    private int m_step;
    public FixedArray(int length)
    {
        m_length = length;
        m_step = sizeof(T);
        m_ptr = (T*)Marshal.AllocHGlobal(m_step * length).ToPointer();
    }
    
    public void Dispose()
    {
        Marshal.FreeHGlobal(new IntPtr(m_ptr));
    }

    public ref T this[int index]
    {
        get
        {
            return ref *(m_ptr + m_step * index);
        }
    }
}


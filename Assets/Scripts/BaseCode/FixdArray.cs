using System;
using System.Runtime.InteropServices;
public unsafe struct FixdCharArray
{
    private char* m_ptr;
    private int m_totalLength;
    private int m_realLength;
    public FixdCharArray(int totalLen)
    {
        IntPtr ptr = Marshal.AllocHGlobal(sizeof(char) * totalLen);
        m_ptr = (char*)ptr.ToPointer();
        m_totalLength = totalLen;
        m_realLength = 0;
    }
    
    public void Dispose()
    {
        Marshal.FreeHGlobal(new IntPtr(m_ptr));
    }

    public void SetValue(char[] charAry)
    {
        
    }
    public char[] GetCharArray()
    {
        char[] res = new char[m_realLength];
        for (int i = 0; i < m_realLength; i++)
        {
            res[i] = *(m_ptr + sizeof(char) * i);
        }
        return res;
    }
}


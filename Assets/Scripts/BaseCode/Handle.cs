
using System;
using UnityEngine;

public struct Handle<T> where T : class
{
    private UInt16 m_index;
    private UInt16 m_magic;
    private static UInt16 s_maxMagic = (1 << 16) - 1;
    private static UInt16 s_autoMagic = 0;
    
    public Handle(UInt16 index)
    {
        if(++s_autoMagic > s_maxMagic)
        {
            s_autoMagic = 1; // 0表示空句柄
        }
        m_index = index;
        m_magic = s_autoMagic;
    }

    public Handle(UInt32 _handle)
    {
        m_index = (UInt16)(_handle >> 16);
        m_magic = (UInt16)_handle;
    }

    public UInt16 GetIndex() { return m_index; }
    public UInt16 GetMagic() { return m_magic; } 
    public UInt32 GetHandle() { return ((UInt32)m_index << 16) | (UInt32)m_magic; }
}

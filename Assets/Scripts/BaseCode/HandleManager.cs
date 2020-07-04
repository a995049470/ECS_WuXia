using System;
using System.Collections.Generic;


public class HandleManager<T> : Single<HandleManager<T>> where T : class, IHandle
{
    private List<T> m_dataList;
    private List<UInt16> m_magicList;
    private List<UInt16> m_freeSoltList;
    
    public HandleManager()
    {
        m_dataList = new List<T>();
        m_magicList = new List<UInt16>();
        m_freeSoltList = new List<UInt16>();
    }

    public T Get(Handle<T> handle)
    {
        T value = null;
        var index = handle.GetIndex();
        if(index < m_dataList.Count && 
           m_magicList[index] == handle.GetMagic())
        {
            value = m_dataList[index];
        }
        return value;
    }

    // public T Get(UInt32 handle)
    // {
    //     T value = null;
    //     UInt16 index = (UInt16)(handle >> 16);
    //     UInt16 magic = (UInt16)handle;
    //     if(index < m_dataList.Count && 
    //        m_magicList[index] == magic)
    //     {
    //         value = m_dataList[index];
    //     }
    //     return value;
    // }

    public Handle<T> Put(T value)
    {
        Handle<T> handle = default;
        if(m_freeSoltList.Count > 0)
        {
            UInt16 index = m_freeSoltList[0];
            m_freeSoltList.RemoveAt(0);
            handle = new Handle<T>(index);
            m_magicList[index] = handle.GetIndex();
            m_dataList[index] = value;
        }
        else
        {
            UInt16 index = (UInt16)m_dataList.Count;
            handle = new Handle<T>(index);
            m_magicList.Add(handle.GetMagic());
            m_dataList.Add(value);
        }
        return handle;
    }

    public void Free(Handle<T> handle)
    {
        var index = handle.GetIndex();
        if(index < m_dataList.Count &&
           m_magicList[index] == handle.GetMagic())
        {
            m_magicList[index] = 0;
            m_dataList[index].OnFree();
            m_dataList[index] = null;
        }
    }

    

}

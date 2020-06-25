using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ResManager : Single<ResManager>
{
    private World m_defaultWorld;
    private GameObjectConversionSettings m_settings;
    private EntityManager m_entityManager;
    private Dictionary<string, Entity> m_entityPrefabDic;
    public ResManager() 
    {
        m_defaultWorld = World.DefaultGameObjectInjectionWorld;
        m_entityManager = m_defaultWorld.EntityManager;
        m_settings = GameObjectConversionSettings.FromWorld(m_defaultWorld, null);
        m_entityPrefabDic = new Dictionary<string, Entity>();
    }
    
    public Entity GetEntityPrefab(string localPath)
    {
        Entity prefab = Entity.Null;
        if(!m_entityPrefabDic.ContainsKey(localPath))
        {
             var go = Load<GameObject>(localPath);
            if(go == null)
            {
                return prefab;
            }
            prefab =  GameObjectConversionUtility.ConvertGameObjectHierarchy(go, m_settings);
            m_entityPrefabDic[localPath] = prefab;
        }
        else
        {
            prefab = m_entityPrefabDic[localPath];
        }
        return prefab;
    }

    public bool TryGetEntity(string localPath, out Entity entity)
    {
        entity = default;
        Entity prefab;
        if(!m_entityPrefabDic.ContainsKey(localPath))
        {
             var go = Load<GameObject>(localPath);
            if(go == null)
            {
                return false;
            }
            prefab =  GameObjectConversionUtility.ConvertGameObjectHierarchy(go, m_settings);
            m_entityPrefabDic[localPath] = prefab;
        }
        else
        {
            prefab = m_entityPrefabDic[localPath];
        }
        entity = m_entityManager.Instantiate(prefab);
        return true;
    }

    public T Load<T>(string localPath) where T : Object
    {
        T value = default(T);
#if UNITY_EDITOR     
        value = Resources.Load<T>(localPath); 
#endif
        return value;
    }

    
}

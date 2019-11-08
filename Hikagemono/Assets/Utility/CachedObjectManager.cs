using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CachedObjectManager : MonoBehaviour
{

    static CachedObjectManager instance = null;
    public static CachedObjectManager Instance
    {
        get
        {
            if (instance == null)
            {
                var instanceGameObject = new GameObject("CachedObjectManager");
                instance = instanceGameObject.AddComponent<CachedObjectManager>();
            }
            return instance;
        }

    }

    IDictionary<string, GameObject> cacheDictionary = new Dictionary<string, GameObject>();
    IDictionary<string, Component> cacheComponent = new Dictionary<string, Component>();

    public void PreCache(GameObject obj)
    {
        if (cacheDictionary.ContainsKey(obj.name)) return;

        cacheDictionary.Add(obj.name, obj);
    }


    public void PreCache<T>(Component component) where T : Component
    {
        if (cacheComponent.ContainsKey(component.name)) return;

        cacheComponent.Add(component.name, component as T);
    }



    public GameObject Borrow(GameObject obj)
    {
        if (cacheDictionary.ContainsKey(obj.name))
        {
            return cacheDictionary[obj.name].gameObject;
        }

        return null;
    }

    public T Borrow<T>(Component component) where T : Component
    {
        if (cacheComponent.ContainsKey(component.name))
        {
            return cacheComponent[component.name] as T;
        }

        return null;
    }

}



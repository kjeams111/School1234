using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericSingletone<T> : MonoBehaviour where T : Component
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<T>();
                if(instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(T).Name;
                    instance = obj.AddComponent<T>();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)   //instance가 Null일때 
        {
            instance = this as T;
            DontDestroyOnLoad(gameObject); //게임 오브젝트가 Scene이 전환되고 파괴되지 않음 
        }
        else
        {
            Destroy(gameObject);  //1개로 유지시키기 위해 생성된 객체를 파괴한다
        }
    }



}

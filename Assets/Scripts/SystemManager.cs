using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemManager : MonoBehaviour
{
    /* <summary>
        싱글톤 인스턴스
        </summary> */
    static SystemManager instance = null;

    public static SystemManager Instance
    {
        get
        {
            return instance;
        }
    }

    void Awake()
    {
        // 유일하게 존재할 수 있도록 에러 처리
        if (instance != null)
        {
            Debug.LogError("SystemManager is initialized twice!");
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    //
    [SerializeField]
    Player player;

    public  Player Hero
    { 
        get 
        { 
            return player; 
        } 
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}

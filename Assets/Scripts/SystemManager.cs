using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemManager : MonoBehaviour
{
    /* <summary>
        �̱��� �ν��Ͻ�
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
        // �����ϰ� ������ �� �ֵ��� ���� ó��
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

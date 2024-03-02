using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemManager : MonoBehaviour
{
    /// <summary>
    /// �̱��� �ν��Ͻ�
    /// </summary>
    static SystemManager instance = null;

    public static SystemManager Instance
    {
        get
        {
            return instance;
        }
    }
    //

    [SerializeField]
    EnemyTable enemyTable;

    public EnemyTable EnemyTable
    {
        get
        {
            return enemyTable;
        }
    }

    BaseSceneMain currentSceneMain;

    public BaseSceneMain CurrentSceneMain
    {
        set
        {
            currentSceneMain = value;
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

        // Scene �̵����� ������� �ʵ��� ó��
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {

        BaseSceneMain baseSceneMain = GameObject.FindObjectOfType<BaseSceneMain>();
        Debug.Log("OnSceneLoaded ! baseSceneMain.name = " + baseSceneMain.name);
        SystemManager.Instance.CurrentSceneMain = baseSceneMain;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public T GetCurrentSceneMain<T>()
         where T : BaseSceneMain
    {
        return currentSceneMain as T;
    }
}
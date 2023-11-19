using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    public const string EnemyPath = "Prefabs/Enemy";

    Dictionary<string, GameObject> EnemyFileCache = new Dictionary<string, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject Load(string resourcePath)
    {
        GameObject go = null;

        if (EnemyFileCache.ContainsKey(resourcePath))   // ĳ�� Ȯ��
        {
            go = EnemyFileCache[resourcePath];
        }
        else
        {
            // ĳ�ÿ� �����Ƿ� �ε�
            go = Resources.Load<GameObject>(resourcePath);
            if (!go)
            {
                Debug.LogError("Load error! path = " + resourcePath);
                return null;
            }
            // �ε� �� ĳ�ÿ� ����
            EnemyFileCache.Add(resourcePath, go);
        }

        GameObject InstancedGO = Instantiate<GameObject>(go);
        return InstancedGO;
    }
}

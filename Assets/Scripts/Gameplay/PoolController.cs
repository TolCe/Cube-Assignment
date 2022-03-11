using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolController : MonoBehaviour
{
    private static PoolController _instance = null;
    public static PoolController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (PoolController)FindObjectOfType(typeof(PoolController));
            }

            return _instance;
        }
    }

    private Dictionary<string, List<GameObject>> _pool;

    private void Awake()
    {
        ResetPool();

    }
    private void OnEnable()
    {
        if (GameEvents.Instance != null)
        {
            GameEvents.Instance.OnLevelLoaded += OnLevelLoaded;
        }
    }
    private void OnDisable()
    {
        if (GameEvents.Instance != null)
        {
            GameEvents.Instance.OnLevelLoaded -= OnLevelLoaded;
        }
    }

    public GameObject TakeFromPool(string objName, Transform spawnParent, Vector3 spawnPoint)
    {
        GameObject obj = null;

        if (!_pool.ContainsKey(objName))
        {
            _pool.Add(objName, new List<GameObject>());
        }

        if (_pool[objName].Count == 0)
        {
            obj = Instantiate(Resources.Load("LevelItems/" + objName) as GameObject);
        }
        else
        {
            obj = _pool[objName][0];
            _pool[objName].RemoveAt(0);
            obj.SetActive(true);
        }

        obj.GetComponent<PoolObject>().SetColliderActivity(true);
        obj.transform.SetParent(spawnParent);
        obj.transform.localPosition = spawnPoint;

        return obj;
    }

    public void PutBackIntoPool(string objName, GameObject objToPut)
    {
        if (!_pool[objName].Contains(objToPut))
        {
            _pool[objName].Add(objToPut);
            objToPut.SetActive(false);
        }
    }

    public void ResetPool()
    {
        _pool = new Dictionary<string, List<GameObject>>();
    }

    private void OnLevelLoaded(int levelIndex)
    {

    }
}

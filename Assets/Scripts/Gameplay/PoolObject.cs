using System;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
    internal Collider _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }
    private void OnEnable()
    {
        if (GameEvents.Instance != null)
        {
            GameEvents.Instance.OnGameStarted += OnGameStarted;
        }
    }

    private void OnDisable()
    {
        if (GameEvents.Instance != null)
        {
            GameEvents.Instance.OnGameStarted -= OnGameStarted;
        }
    }

    public void SetColliderActivity(bool state)
    {
        if (_collider != null)
        {
            _collider.enabled = state;
        }
    }

    private void OnGameStarted()
    {
        //PoolController.Instance.PutBackIntoPool(VO.ItemName, gameObject);
    }
}

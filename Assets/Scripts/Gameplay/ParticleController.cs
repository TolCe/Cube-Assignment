using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [SerializeField] private ParticleSystem _crashParticle, _coinParticle;

    private void OnEnable()
    {
        if (GameEvents.Instance != null)
        {
            GameEvents.Instance.OnCoinEarned += OnCoinEarned;
            GameEvents.Instance.OnObstacleTriggered += OnObstacleTriggered;
        }
    }

    private void OnDisable()
    {
        if (GameEvents.Instance != null)
        {
            GameEvents.Instance.OnCoinEarned -= OnCoinEarned;
            GameEvents.Instance.OnObstacleTriggered -= OnObstacleTriggered;
        }
    }

    private void OnCoinEarned(int obj)
    {
        _coinParticle.Play();
    }

    private void OnObstacleTriggered(float arg1, float arg2)
    {
        _crashParticle.Play();
    }
}

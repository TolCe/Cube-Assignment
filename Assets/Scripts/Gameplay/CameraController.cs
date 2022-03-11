using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _zOffset;
    [SerializeField] private float _followSpeed = 5;
    private Vector3 _initPos;

    private void Awake()
    {
        _initPos = transform.position;
    }
    private void OnEnable()
    {
        if (GameEvents.Instance != null)
        {
            GameEvents.Instance.OnGameStarted += OnGameStarted;
            GameEvents.Instance.OnForwardMove += OnForwardMove;
        }
    }
    private void OnDisable()
    {
        if (GameEvents.Instance != null)
        {
            GameEvents.Instance.OnGameStarted -= OnGameStarted;
            GameEvents.Instance.OnForwardMove -= OnForwardMove;
        }
    }

    private void OnGameStarted()
    {
        transform.position = _initPos;
    }
    private void OnForwardMove(float playerZPos)
    {
        Vector3 targetPos = transform.position;
        targetPos.z = playerZPos + _zOffset;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, _followSpeed * Time.fixedDeltaTime);
    }
}

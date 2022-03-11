using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _zOffset;
    [SerializeField] private float _followSpeed = 5;
    [SerializeField] private float _successRotateTime = 2;
    private Vector3 _initPos;
    private Quaternion _initRot;

    private void Awake()
    {
        _initPos = transform.position;
        _initRot = transform.rotation;
    }
    private void OnEnable()
    {
        if (GameEvents.Instance != null)
        {
            GameEvents.Instance.OnGameStarted += OnGameStarted;
            GameEvents.Instance.OnForwardMove += OnForwardMove;
            GameEvents.Instance.OnLevelSuccess += OnLevelSuccess;
        }
    }
    private void OnDisable()
    {
        if (GameEvents.Instance != null)
        {
            GameEvents.Instance.OnGameStarted -= OnGameStarted;
            GameEvents.Instance.OnForwardMove -= OnForwardMove;
            GameEvents.Instance.OnLevelSuccess -= OnLevelSuccess;
        }
    }

    private void OnGameStarted()
    {
        transform.position = _initPos;
        transform.rotation = _initRot;
    }
    private void OnForwardMove(float playerZPos)
    {
        Vector3 targetPos = transform.position;
        targetPos.z = playerZPos + _zOffset;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, _followSpeed * Time.fixedDeltaTime);
    }

    private void OnLevelSuccess()
    {
        StartCoroutine(RotateToSuccess());
    }

    private IEnumerator RotateToSuccess()
    {
        float timer = 0;
        Vector3 targetPos = transform.position - 2 * Vector3.up - 2 * _zOffset * Vector3.forward;
        float distance = Vector3.Distance(targetPos, transform.position);
        float angle = Quaternion.Angle(transform.rotation, Quaternion.Euler(0, 180, 0));
        while (timer < _successRotateTime)
        {
            timer += Time.fixedDeltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPos, distance / _successRotateTime * Time.fixedDeltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 180, 0), angle / _successRotateTime * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }
    }
}

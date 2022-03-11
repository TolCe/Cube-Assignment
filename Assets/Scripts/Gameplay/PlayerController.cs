using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerContainer PlayerContainer;

    private bool _shouldMoveForward;
    private float _initialPlayerX;
    [SerializeField] private Animator _animator;
    private float _health;

    private void OnEnable()
    {
        if (GameEvents.Instance != null)
        {
            GameEvents.Instance.OnGameStarted += OnGameStarted;
            GameEvents.Instance.OnStartedPlaying += OnStartedPlaying;
            GameEvents.Instance.OnHorizontalMove += MoveHorizontal;
            GameEvents.Instance.OnResetInput += OnResetInput;
            GameEvents.Instance.OnBuyItem += OnBuyItem;
            GameEvents.Instance.OnLevelFinished += OnLevelFinished;
            GameEvents.Instance.OnLevelSuccess += OnLevelSuccess;
        }
    }

    private void OnDisable()
    {
        if (GameEvents.Instance != null)
        {
            GameEvents.Instance.OnGameStarted -= OnGameStarted;
            GameEvents.Instance.OnStartedPlaying -= OnStartedPlaying;
            GameEvents.Instance.OnHorizontalMove -= MoveHorizontal;
            GameEvents.Instance.OnResetInput -= OnResetInput;
            GameEvents.Instance.OnBuyItem -= OnBuyItem;
            GameEvents.Instance.OnLevelFinished -= OnLevelFinished;
            GameEvents.Instance.OnLevelSuccess -= OnLevelSuccess;
        }
    }

    private void FixedUpdate()
    {
        if (_shouldMoveForward)
        {
            transform.Translate(PlayerContainer.VO.ForwardSpeed * Time.fixedDeltaTime * Vector3.forward);
            GameEvents.Instance.ForwardMove(transform.position.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PoolObject item = other.GetComponent<PoolObject>();
        if (item is ObstacleEntity obstacle)
        {
            obstacle.GetHit();
            SetHealth(-obstacle.VO.Damage + PlayerContainer.VO.Armour);
            GameEvents.Instance.ObstacleTriggered(_health, PlayerContainer.VO.Health);
        }
        else if (item is FinishEntity finish)
        {
            GameEvents.Instance.LevelFinished();
            GameEvents.Instance.LevelSuccess();
        }
        else if (item is CollectableEntity collectable)
        {
            collectable.GetHit();
            GameEvents.Instance.CoinTriggered(collectable.VO.Prize);
        }
    }

    public void MoveHorizontal(float moveAmount, float xLimit)
    {
        if ((transform.position.x > xLimit && moveAmount > 0) || (transform.position.x < -xLimit && moveAmount < 0))
        {
            GameEvents.Instance.ResetInput();
            return;
        }

        Vector3 targetPosition = transform.position;
        targetPosition.x = _initialPlayerX + moveAmount;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, PlayerContainer.VO.HorizontalFollowSpeed * Time.fixedDeltaTime);
        transform.eulerAngles = 20f * (targetPosition.x - transform.position.x) * Vector3.up;
    }

    private void SetHealth(float change)
    {
        _health += change;
        if (_health <= 0)
        {
            _animator.SetBool(PlayerAnimationParameters.Run.ToString(), false);
            GameEvents.Instance.LevelFinished();
            GameEvents.Instance.LevelFail();
        }
    }

    private void OnGameStarted()
    {
        _health = PlayerContainer.VO.Health;
        transform.position = Vector3.zero;
        _animator.SetBool(PlayerAnimationParameters.Run.ToString(), false);
        _animator.SetBool(PlayerAnimationParameters.Victory.ToString(), false);
    }
    private void OnStartedPlaying()
    {
        _shouldMoveForward = true;
        _animator.SetBool(PlayerAnimationParameters.Run.ToString(), true);
    }

    private void OnResetInput()
    {
        _initialPlayerX = transform.position.x;
    }

    private void OnLevelFinished()
    {
        _shouldMoveForward = false;
    }

    private void OnLevelSuccess()
    {
        _animator.SetBool(PlayerAnimationParameters.Victory.ToString(), true);
        StartCoroutine(MoveZeroX());
    }

    private void OnBuyItem(MarketItems itemType)
    {
        switch (itemType)
        {
            case MarketItems.Health:
                PlayerContainer.VO.Health += 10;
                break;
            case MarketItems.Armour:
                PlayerContainer.VO.Armour += 1;
                break;
        }

        GameEvents.Instance.PlayerDataLevelIncreased(itemType);
    }
    private IEnumerator MoveZeroX()
    {
        Vector3 targetPos = transform.position;
        targetPos.x = 0;
        while (Vector3.Distance(targetPos, transform.position) > 0.2f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, 5 * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }
    }
}
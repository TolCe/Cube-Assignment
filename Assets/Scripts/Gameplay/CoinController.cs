using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    private int _coinAmount;
    private int _coinEarnedInThisLevel;

    private void OnEnable()
    {
        if (GameEvents.Instance != null)
        {
            GameEvents.Instance.OnLevelLoaded += OnLevelLoaded;
            GameEvents.Instance.OnCoinTriggered += OnCoinTriggered;
            GameEvents.Instance.OnLevelSuccess += OnLevelSuccess;
            GameEvents.Instance.OnTryToBuyItem += OnTryToBuyItem;
        }
    }
    private void OnDisable()
    {
        if (GameEvents.Instance != null)
        {
            GameEvents.Instance.OnLevelLoaded -= OnLevelLoaded;
            GameEvents.Instance.OnCoinTriggered -= OnCoinTriggered;
            GameEvents.Instance.OnLevelSuccess -= OnLevelSuccess;
            GameEvents.Instance.OnTryToBuyItem -= OnTryToBuyItem;
        }
    }

    private void OnCoinTriggered(int amount)
    {
        _coinEarnedInThisLevel += amount;
        GameEvents.Instance.CoinEarned(_coinEarnedInThisLevel);
    }

    private void CalculateCoinAmount()
    {
        _coinAmount = PlayerPrefs.GetInt("Coin", 0);
        GameEvents.Instance.CoinCalculated(_coinAmount);
    }

    private void OnLevelLoaded(int levelIndex)
    {
        _coinEarnedInThisLevel = 0;
        CalculateCoinAmount();
    }

    private void OnLevelSuccess()
    {
        _coinAmount += _coinEarnedInThisLevel;
        PlayerPrefs.SetInt("Coin", _coinAmount);
        CalculateCoinAmount();
    }

    private void OnTryToBuyItem(int price, MarketItems itemType)
    {
        if (price <= _coinAmount)
        {
            GameEvents.Instance.BuyItem(itemType);
            _coinAmount -= price;
            PlayerPrefs.SetInt("Coin", _coinAmount);
            CalculateCoinAmount();
        }
    }
}

using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    private static GameEvents _instance = null;
    public static GameEvents Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (GameEvents)FindObjectOfType(typeof(GameEvents));
            }

            return _instance;
        }
    }

    public event Action OnGameStarted;
    public void GameStarted()
    {
        if (OnGameStarted != null)
        {
            OnGameStarted();
        }
    }
    public event Action<int> OnLevelLoaded;
    public void LevelLoaded(int levelIndex)
    {
        if (OnLevelLoaded != null)
        {
            OnLevelLoaded(levelIndex);
        }
    }
    public event Action OnStartedPlaying;
    public void StartedPlaying()
    {
        if (OnStartedPlaying != null)
        {
            OnStartedPlaying();
        }
    }
    public event Action OnResetInput;
    public void ResetInput()
    {
        if (OnResetInput != null)
        {
            OnResetInput();
        }
    }
    public event Action<float, float> OnHorizontalMove;
    public void HorizontalMove(float input, float xLimit)
    {
        if (OnHorizontalMove != null)
        {
            OnHorizontalMove(input, xLimit);
        }
    }
    public event Action<float> OnForwardMove;
    public void ForwardMove(float playerZPos)
    {
        if (OnForwardMove != null)
        {
            OnForwardMove(playerZPos);
        }
    }
    public event Action<int> OnCoinTriggered;
    public void CoinTriggered(int earnedAmount)
    {
        if (OnCoinTriggered != null)
        {
            OnCoinTriggered(earnedAmount);
        }
    }
    public event Action<int> OnCoinEarned;
    public void CoinEarned(int earnedAmount)
    {
        if (OnCoinEarned != null)
        {
            OnCoinEarned(earnedAmount);
        }
    }
    public event Action<int> OnCoinCalculated;
    public void CoinCalculated(int coinAmount)
    {
        if (OnCoinCalculated != null)
        {
            OnCoinCalculated(coinAmount);
        }
    }
    public event Action<int, MarketItems> OnTryToBuyItem;
    public void TryToBuyItem(int price, MarketItems itemType)
    {
        if (OnTryToBuyItem != null)
        {
            OnTryToBuyItem(price, itemType);
        }
    }
    public event Action<MarketItems> OnBuyItem;
    public void BuyItem(MarketItems itemType)
    {
        if (OnBuyItem != null)
        {
            OnBuyItem(itemType);
        }
    }
    public event Action<MarketItems> OnPlayerDataLevelIncreased;
    public void PlayerDataLevelIncreased(MarketItems itemType)
    {
        if (OnPlayerDataLevelIncreased != null)
        {
            OnPlayerDataLevelIncreased(itemType);
        }
    }
    public event Action OnLevelFinished;
    public void LevelFinished()
    {
        if (OnLevelFinished != null)
        {
            OnLevelFinished();
        }
    }
    public event Action OnLevelSuccess;
    public void LevelSuccess()
    {
        if (OnLevelSuccess != null)
        {
            OnLevelSuccess();
        }
    }
    public event Action OnLevelFail;
    public void LevelFail()
    {
        if (OnLevelFail != null)
        {
            OnLevelFail();
        }
    }
    public event Action OnLevelRestart;
    public void LevelRestart()
    {
        if (OnLevelRestart != null)
        {
            OnLevelRestart();
        }
    }
    public event Action OnNextLevel;
    public void NextLevel()
    {
        if (OnNextLevel != null)
        {
            OnNextLevel();
        }
    }
}

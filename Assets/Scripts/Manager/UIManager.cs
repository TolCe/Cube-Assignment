using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _startElements;
    [SerializeField] private GameObject[] _gameplayElements;
    [SerializeField] private GameObject[] _successElements;
    [SerializeField] private GameObject[] _failElements;

    [SerializeField] private TMP_Text[] _levelTexts;
    [SerializeField] private TMP_Text[] _totalCoinAmountTexts;
    [SerializeField] private TMP_Text[] _coinEarnedTexts;

    [SerializeField] private Image HealthImage;

    private void OnEnable()
    {
        if (GameEvents.Instance != null)
        {
            GameEvents.Instance.OnLevelLoaded += OnLevelLoaded;
            GameEvents.Instance.OnObstacleTriggered += OnObstacleTriggered;
            GameEvents.Instance.OnLevelFinished += OnLevelFinished;
            GameEvents.Instance.OnLevelSuccess += OnLevelSuccess;
            GameEvents.Instance.OnLevelFail += OnLevelFail;
            GameEvents.Instance.OnStartedPlaying += OnStartedPlaying;
            GameEvents.Instance.OnCoinCalculated += OnCoinCalculated;
            GameEvents.Instance.OnCoinEarned += OnCoinEarned;
        }
    }

    private void OnDisable()
    {
        if (GameEvents.Instance != null)
        {
            GameEvents.Instance.OnLevelLoaded -= OnLevelLoaded;
            GameEvents.Instance.OnObstacleTriggered -= OnObstacleTriggered;
            GameEvents.Instance.OnLevelFinished -= OnLevelFinished;
            GameEvents.Instance.OnLevelSuccess -= OnLevelSuccess;
            GameEvents.Instance.OnLevelFail -= OnLevelFail;
            GameEvents.Instance.OnStartedPlaying -= OnStartedPlaying;
            GameEvents.Instance.OnCoinCalculated -= OnCoinCalculated;
            GameEvents.Instance.OnCoinEarned -= OnCoinEarned;
        }
    }

    private void OnLevelLoaded(int levelIndex)
    {
        SetScreenActivity(_startElements, true);
        SetScreenActivity(_gameplayElements, false);
        SetScreenActivity(_successElements, false);
        SetScreenActivity(_failElements, false);

        ChangeTexts(_levelTexts, "Level " + (levelIndex + 1));
        ChangeTexts(_coinEarnedTexts, "");
        HealthImage.fillAmount = 1;
    }

    private void OnStartedPlaying()
    {
        SetScreenActivity(_startElements, false);
        SetScreenActivity(_gameplayElements, true);
    }

    private void OnCoinCalculated(int coinAmount)
    {
        ChangeTexts(_totalCoinAmountTexts, "" + coinAmount);
    }

    private void OnCoinEarned(int coinEarned)
    {
        ChangeTexts(_coinEarnedTexts, "+" + coinEarned);
    }

    private void SetScreenActivity(GameObject[] screen, bool state)
    {
        foreach (var item in screen)
        {
            item.SetActive(state);
        }
    }
    private void ChangeTexts(TMP_Text[] texts, string aMessage)
    {
        foreach (var item in texts)
        {
            item.text = aMessage;
        }
    }

    public void OnLevelFinished()
    {
        SetScreenActivity(_gameplayElements, false);
    }
    public void OnLevelSuccess()
    {
        SetScreenActivity(_successElements, true);
    }
    public void OnLevelFail()
    {
        SetScreenActivity(_failElements, true);
    }

    public void OnNextLevelPressed()
    {
        GameEvents.Instance.NextLevel();
    }
    public void OnRestartPressed()
    {
        GameEvents.Instance.LevelRestart();
    }

    public void OnStartPressed()
    {
        GameEvents.Instance.StartedPlaying();
    }

    private void OnObstacleTriggered(float health, float totalHealth)
    {
        HealthImage.fillAmount = health / totalHealth;
    }
}

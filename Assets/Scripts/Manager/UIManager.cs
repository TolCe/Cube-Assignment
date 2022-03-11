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

    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private TMP_Text[] _totalCoinAmountTexts;
    [SerializeField] private TMP_Text[] _coinEarnedTexts;

    private void OnEnable()
    {
        if (GameEvents.Instance != null)
        {
            GameEvents.Instance.OnLevelLoaded += OnLevelLoaded;
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

        _levelText.text = "Level " + (levelIndex + 1);
    }

    private void OnStartedPlaying()
    {
        SetScreenActivity(_startElements, false);
        SetScreenActivity(_gameplayElements, true);
    }

    private void OnCoinCalculated(int coinAmount)
    {
        foreach (var item in _totalCoinAmountTexts)
        {
            item.text = "Total Coins: " + coinAmount;
        }
    }

    private void OnCoinEarned(int coinEarned)
    {
        foreach (var item in _coinEarnedTexts)
        {
            item.text = "+" + coinEarned;
        }
    }

    private void SetScreenActivity(GameObject[] screen, bool state)
    {
        foreach (var item in screen)
        {
            item.SetActive(state);
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
}

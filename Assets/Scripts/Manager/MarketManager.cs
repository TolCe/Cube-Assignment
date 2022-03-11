using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MarketManager : MonoBehaviour
{
    public PlayerContainer PlayerContainer;
    public MarketContainer MarketContainer;
    public TMP_Text TotalHealthText, TotalArmourText;
    public Text HealthPriceText, ArmourPriceText;

    private void OnEnable()
    {
        if (GameEvents.Instance != null)
        {
            GameEvents.Instance.OnLevelLoaded += OnLevelLoaded;
            GameEvents.Instance.OnPlayerDataLevelIncreased += OnPlayerDataLevelIncreased;
        }
    }

    private void OnDisable()
    {
        if (GameEvents.Instance != null)
        {
            GameEvents.Instance.OnLevelLoaded += OnLevelLoaded;
            GameEvents.Instance.OnPlayerDataLevelIncreased -= OnPlayerDataLevelIncreased;
        }
    }

    private void OnLevelLoaded(int levelIndex)
    {
        UpdateText();
    }

    public void OnHealthBought()
    {
        GameEvents.Instance.TryToBuyItem(MarketContainer.VO.BaseHealthPrice, MarketItems.Health);
    }
    public void OnArmourBought()
    {
        GameEvents.Instance.TryToBuyItem(MarketContainer.VO.BaseArmourPrice, MarketItems.Armour);
    }

    private void UpdateText()
    {
        TotalHealthText.text = PlayerContainer.VO.Health + "";
        TotalArmourText.text = PlayerContainer.VO.Armour + "";
        HealthPriceText.text = MarketContainer.VO.BaseHealthPrice + " Coin";
        ArmourPriceText.text = MarketContainer.VO.BaseArmourPrice + " Coin";
    }

    private void OnPlayerDataLevelIncreased(MarketItems itemType)
    {
        switch (itemType)
        {
            case MarketItems.Health:
                MarketContainer.VO.BaseHealthPrice++;
                break;
            case MarketItems.Armour:
                MarketContainer.VO.BaseArmourPrice++;
                break;
        }

        UpdateText();
    }
}

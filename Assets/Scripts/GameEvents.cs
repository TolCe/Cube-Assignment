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

    public event Action OnLevelLoaded;
    public void LevelLoaded()
    {
        if (OnLevelLoaded != null)
        {
            OnLevelLoaded();
        }
    }
}

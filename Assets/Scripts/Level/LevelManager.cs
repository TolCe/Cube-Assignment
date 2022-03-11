using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private LevelContainer _levelContainer;

    private int _levelIndexClamped;
    private int _currentlevelIndex;

    private List<CollectableEntity> _levelsCollectableObjects;
    private List<FinishEntity> _levelsFinishObjects;
    private List<ObstacleEntity> _levelsObstacleObjects;
    private List<RoadEntity> _levelsRoadObjects;

    private void Awake()
    {
        _levelsCollectableObjects = new List<CollectableEntity>();
        _levelsFinishObjects = new List<FinishEntity>();
        _levelsObstacleObjects = new List<ObstacleEntity>();
        _levelsRoadObjects = new List<RoadEntity>();

        _levelIndexClamped = PlayerPrefs.GetInt("LEVELINDEX", 0);
        _currentlevelIndex = PlayerPrefs.GetInt("CURRENTLEVELINDEX", 0);
    }
    private void OnEnable()
    {
        if (GameEvents.Instance != null)
        {
            GameEvents.Instance.OnGameStarted += OnGameStarted;
            GameEvents.Instance.OnLevelSuccess += OnLevelSuccess;
            GameEvents.Instance.OnNextLevel += OnNextLevel;
            GameEvents.Instance.OnLevelRestart += OnLevelRestart;
        }
    }
    private void OnDisable()
    {
        if (GameEvents.Instance != null)
        {
            GameEvents.Instance.OnGameStarted -= OnGameStarted;
            GameEvents.Instance.OnLevelSuccess -= OnLevelSuccess;
            GameEvents.Instance.OnNextLevel -= OnNextLevel;
            GameEvents.Instance.OnLevelRestart -= OnLevelRestart;
        }
    }

    private void Start()
    {
        LoadLevel();
    }

    public void NextLevel()
    {
        _currentlevelIndex++;
        if (_currentlevelIndex >= _levelContainer.Levels.Count)
        {
            int _randLevel = Random.Range(0, _levelContainer.Levels.Count);
            _levelIndexClamped = _randLevel;
        }
        else
        {
            _levelIndexClamped = _currentlevelIndex;
        }

        PlayerPrefs.SetInt("CURRENTLEVELINDEX", _currentlevelIndex);
        PlayerPrefs.SetInt("LEVELINDEX", _levelIndexClamped);
        LoadLevel();
    }

    public void LoadLevel()
    {
        GameEvents.Instance.GameStarted();
    }

    private void OnGameStarted()
    {
        foreach (var item in _levelsCollectableObjects)
        {
            PoolController.Instance.PutBackIntoPool(item.VO.ItemName.ToString(), item.gameObject);
        }
        foreach (var item in _levelsFinishObjects)
        {
            PoolController.Instance.PutBackIntoPool(item.VO.ItemName.ToString(), item.gameObject);
        }
        foreach (var item in _levelsObstacleObjects)
        {
            PoolController.Instance.PutBackIntoPool(item.VO.ItemName.ToString(), item.gameObject);
        }
        foreach (var item in _levelsRoadObjects)
        {
            PoolController.Instance.PutBackIntoPool(item.VO.ItemName.ToString(), item.gameObject);
        }

        _levelsCollectableObjects = new List<CollectableEntity>();
        _levelsFinishObjects = new List<FinishEntity>();
        _levelsObstacleObjects = new List<ObstacleEntity>();
        _levelsRoadObjects = new List<RoadEntity>();

        LevelVO currentLevel = _levelContainer.Levels[_levelIndexClamped];
        for (int i = 0; i < currentLevel.RoadAmount; i++)
        {
            _levelsRoadObjects.Add(PoolController.Instance.TakeFromPool(currentLevel.Road.ItemName.ToString(), transform, (i * currentLevel.IntervalBetweenRoads - 5) * Vector3.forward).GetComponent<RoadEntity>());
        }
        foreach (var item in currentLevel.Barriers)
        {
            _levelsObstacleObjects.Add(PoolController.Instance.TakeFromPool(item.ItemName.ToString(), transform, item.Position).GetComponent<ObstacleEntity>());
        }
        foreach (var item in currentLevel.Collectables)
        {
            _levelsCollectableObjects.Add(PoolController.Instance.TakeFromPool(item.ItemName.ToString(), transform, item.Position).GetComponent<CollectableEntity>());
        }
        ItemVO finishItem = currentLevel.Finish;
        _levelsFinishObjects.Add(PoolController.Instance.TakeFromPool(finishItem.ItemName.ToString(), transform, finishItem.Position).GetComponent<FinishEntity>());

        GameEvents.Instance.LevelLoaded(_currentlevelIndex);
    }

    private void OnLevelSuccess()
    {

    }

    private void OnLevelRestart()
    {
        LoadLevel();
    }

    private void OnNextLevel()
    {
        NextLevel();
    }

}

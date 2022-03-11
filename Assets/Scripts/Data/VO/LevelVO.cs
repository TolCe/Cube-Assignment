using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelVO
{
    public int RoadAmount;
    public float IntervalBetweenRoads;
    public ItemVO Finish;
    public ItemVO Road;
    public List<ItemVO> Collectables;
    public List<ItemVO> Barriers;
}

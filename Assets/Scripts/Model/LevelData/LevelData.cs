using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelData
{
    public List<TileData> tileData;
    public int boardWidth;
    public int boardHeight;
    public int playerCount;
}

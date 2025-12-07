using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapData", menuName = "Scriptable Objects/MapData")]
public class MapData : ScriptableObject
{
    public List<TileData> tiles;
    public List<WallData> walls;

    [Header("Heart Spawn Coords")]
    public Vector2Int heart1;
    public Vector2Int heart2;
}

[System.Serializable]
public class TileData
{
    public int x, y;
    public bool walkable = true;
}

[System.Serializable]
public class WallData
{
    public int xA, yA;
    public int xB, yB;
}

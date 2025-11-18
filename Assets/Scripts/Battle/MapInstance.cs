using System.Collections.Generic;
using UnityEngine;

public class MapInstance
{
    public MapData source;

    public Dictionary<(int x, int y), TileInstance> tiles = new();
    public HashSet<WallInstance> walls = new();

    public int maxX;
    public int maxY;
    public MapInstance(MapData mapData)
    {
        source = mapData;
    }
}

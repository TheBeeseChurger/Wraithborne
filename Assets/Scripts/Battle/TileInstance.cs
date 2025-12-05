using System.Collections.Generic;
using UnityEngine;

public class TileInstance
{
    public readonly TileData source;

    public int tileX;
    public int tileY;

    public List<TileInstance> neighbors = new();
    public UnitInstance occupant;

    public readonly bool walkable;

    public TileInstance(TileData tileData)
    {
        source = tileData;

        tileX = tileData.x;
        tileY = tileData.y;

        walkable = tileData.walkable;
    }

    public bool IsBlocked() => !walkable || occupant != null;
}

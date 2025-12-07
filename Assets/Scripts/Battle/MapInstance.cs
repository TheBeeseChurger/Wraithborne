using System;
using System.Collections.Generic;
using UnityEngine;

public class MapInstance
{
    public readonly MapData source;

    public Dictionary<(int x, int y), TileInstance> tiles = new();
    public HashSet<WallInstance> walls = new();
    public HashSet<UnitInstance> units = new();

    public event Action<UnitInstance> UnitSpawned;

    public int maxX;
    public int maxY;

    private int _heartsMade = 0;

    public MapInstance(MapData mapData)
    {
        source = mapData;
    }

    public void RefreshMap()
    {
        int smallX = source.tiles[0].x;
        int largeX = source.tiles[0].x;
        int smallY = source.tiles[0].y;
        int largeY = source.tiles[0].y;

        foreach (var t in source.tiles)
        {
            var ti = new TileInstance(t);
            tiles[(t.x, t.y)] = ti;

            if (t.x < smallX) smallX = t.x;
            else if (t.x > largeX) largeX = t.x;

            if (t.y < smallY) smallY = t.y;
            else if (t.y > largeY) largeY = t.y;
        }

        maxX = largeX - smallX;
        maxY = largeY - smallY;

        foreach (var tile in tiles.Values)
        {
            TryAddNeighbor(1, 0, tile);
            TryAddNeighbor(-1, 0, tile);
            TryAddNeighbor(0, 1, tile);
            TryAddNeighbor(0, -1, tile);
        }

        foreach(var w in source.walls)
        {
            var wi = new WallInstance(w);

            walls.Add(wi);

            if (tiles.TryGetValue((w.xA, w.yA), out TileInstance a) && tiles.TryGetValue((w.xB, w.yB), out TileInstance b))
            {
                if (a.neighbors.Contains(b)) a.neighbors.Remove(b);
                if (b.neighbors.Contains(a)) b.neighbors.Remove(a);
            }
        }
    }

    public void SpawnUnit(CardInstance card, int x, int y, PlayerRuntimeState owner = null)
    {
        if (tiles.TryGetValue((x, y), out TileInstance ti))
        {
            var unit = new UnitInstance(card, ti, owner);
            units.Add(unit);
            UnitSpawned?.Invoke(unit);
        }
        else
        {
            Debug.LogError("Tile to spawn unit on is not valid!");
            return;
        }
    }

    public void SpawnHeartUnit(CardInstance heartCard, PlayerRuntimeState owner)
    {
        if (_heartsMade >= 2) return;
        else if (_heartsMade == 1)
        {
            _heartsMade++;

            var x = source.heart2.x;
            var y = source.heart2.y;
            SpawnUnit(heartCard, x, y, owner);
        }
        else if (_heartsMade == 0)
        {
            _heartsMade++;

            var x = source.heart1.x;
            var y = source.heart1.y;
            SpawnUnit(heartCard, x, y, owner);
        }
    }

    private void TryAddNeighbor(int dx, int dy, TileInstance instance)
    {
        var key = (instance.tileX + dx, instance.tileY + dy);
        if (tiles.TryGetValue(key, out var neighbor)) instance.neighbors.Add(neighbor);
    }
}

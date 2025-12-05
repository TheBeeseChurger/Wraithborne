using System.Collections.Generic;
using UnityEngine;

public class MapInstance
{
    public readonly MapData source;

    public Dictionary<(int x, int y), TileInstance> tiles = new();
    public HashSet<WallInstance> walls = new();

    public int maxX;
    public int maxY;

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
            var a = tiles[(w.xA, w.yA)];
            var b = tiles[(w.xB, w.yB)];

            var wi = new WallInstance(w);

            walls.Add(wi);

            if (a != null && b != null)
            {
                if (a.neighbors.Contains(b)) a.neighbors.Remove(b);
                if (b.neighbors.Contains(a)) b.neighbors.Remove(a);
            }
        }
    }

    private void TryAddNeighbor(int dx, int dy, TileInstance instance)
    {
        var key = (instance.tileX + dx, instance.tileY + dy);
        if (tiles.TryGetValue(key, out var neighbor)) instance.neighbors.Add(neighbor);
    }
}

using UnityEngine;

public class MapLayoutController : MonoBehaviour
{
    public static MapLayoutController Instance { get; private set; }

    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private GameObject wallPrefab;

    private MapInstance _instance;

    private void Awake()
    {
        Instance = this;
    }

    public void Initialize(MapInstance mapInstance)
    {
        _instance = mapInstance;
        SpawnMap();
    }

    private void SpawnMap()
    {
        float posX = -(_instance.maxX / 2f);
        float posY = -(_instance.maxY / 2f);

        transform.position = new Vector3(posX, 0f, posY);

        foreach (var tile in _instance.tiles)
        {
            var inst = Instantiate<GameObject>(tilePrefab);
            inst.transform.SetParent(transform);

            // At some point, pass the data on to the prefab
            inst.transform.localPosition = new Vector3(tile.Value.tileX, 0f, tile.Value.tileY);
        }

        foreach (var wall in _instance.walls)
        {
            var inst = Instantiate<GameObject>(wallPrefab);
            inst.transform.SetParent(transform);

            float xPos = (wall.source.xA + wall.source.xB) / 2f;
            float yPos = (wall.source.yA + wall.source.yB) / 2f;

            inst.transform.localPosition = new Vector3(xPos, 0f, yPos);

            float diffX = wall.source.xA - wall.source.xB;

            if (diffX != 0f)
            {
                inst.transform.localEulerAngles = new Vector3(0f, 90f, 0f);
            }
        }
    }
}

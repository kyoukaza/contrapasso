using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallGenerator : MonoBehaviour
{
public Image fadeOverlay;

[Header("Item/Trap Generation")]
public GameObject[] itemPrefabs;
public GameObject[] trapPrefabs;
public GameObject ladderPrefab;

public int minItems = 1;
public int maxItems = 5;
public int minTraps = 3;
public int maxTraps = 6;

private List<Vector2Int> openTiles = new List<Vector2Int>();

    class Region
    {
        public int x, y, width, height;
        public Region a, b;
        public bool verticalSplit;
        public int split; 
        public int doorPos;
    }

    [Header("Grid Settings (must be odd)")]
    public int gridSize = 81;
    public int minRoomSize = 7;
    [Header("Wall Prefab (1×1 cube)")]
    public GameObject wallPrefab;
    public float cellSize = 1f;

    private Region root;

    void Start()
    {
         Generate();
    }

    void Partition(Region region)
    {
        bool canVert = region.width  >= minRoomSize * 2;
        bool canHorz = region.height >= minRoomSize * 2;
        if (!canVert && !canHorz) return;

        region.verticalSplit = canVert && (!canHorz || Random.value > 0.5f);

        if (region.verticalSplit)
        {
            region.split = Random.Range(region.x + minRoomSize,
                                       region.x + region.width  - minRoomSize + 1);
            region.a = new Region {
                x = region.x,
                y = region.y,
                width  = region.split - region.x,
                height = region.height
            };
            region.b = new Region {
                x = region.split,
                y = region.y,
                width  = region.x + region.width - region.split,
                height = region.height
            };
            region.doorPos = Random.Range(region.y + 1, region.y + region.height - 1);
        }
        else
        {
            region.split = Random.Range(region.y + minRoomSize,
                                       region.y + region.height - minRoomSize + 1);
            region.a = new Region {
                x = region.x,
                y = region.y,
                width  = region.width,
                height = region.split - region.y
            };
            region.b = new Region {
                x = region.x,
                y = region.split,
                width  = region.width,
                height = region.y + region.height - region.split
            };
            region.doorPos = Random.Range(region.x + 1, region.x + region.width - 1);
        }

        Partition(region.a);
        Partition(region.b);
    }

void SpawnWalls(Region region)
{
    if (region.a != null && region.b != null)
    {
        if (region.verticalSplit)
        {
            int x = region.split;
            for (int y = region.y; y < region.y + region.height; y++)
            {
                if (y == region.doorPos || y == region.doorPos + 1) continue;
                SpawnWallAt(x, y);
            }
        }
        else
        {
            int y = region.split;
            for (int x = region.x; x < region.x + region.width; x++)
            {
                if (x == region.doorPos || x == region.doorPos + 1) continue;
                SpawnWallAt(x, y);
            }
        }

        SpawnWalls(region.a);
        SpawnWalls(region.b);
    }
}


    void SpawnWallAt(int gx, int gy)
    {
        float offset = (gridSize - 1) / 2f * cellSize;
        Vector3 pos = new Vector3(
            gx * cellSize - offset,
            0f,
            gy * cellSize - offset
        );
        Instantiate(wallPrefab, pos, Quaternion.identity, transform);
        openTiles.Remove(new Vector2Int(gx, gy));
    }

    public void Generate()
{
    foreach (Transform child in transform)
    {
        Destroy(child.gameObject);
    }

    openTiles.Clear();
    for (int x = 0; x < gridSize; x++)
    {
        for (int y = 0; y < gridSize; y++)
        {
            openTiles.Add(new Vector2Int(x, y));
        }
    }

    root = new Region { x = 0, y = 0, width = gridSize, height = gridSize };
    Partition(root);
    SpawnWalls(root);
    SpawnObjects();
}

void SpawnObjects()
{
    float offset = (gridSize - 1) / 2f * cellSize;

    List<Vector2Int> shuffled = new List<Vector2Int>(openTiles);
    for (int i = 0; i < shuffled.Count; i++)
    {
        Vector2Int temp = shuffled[i];
        int rand = Random.Range(i, shuffled.Count);
        shuffled[i] = shuffled[rand];
        shuffled[rand] = temp;
    }

    int itemCount = Random.Range(minItems, maxItems + 1);
    int trapCount = Random.Range(minTraps, maxTraps + 1);

    int used = 0;

    // Items
    for (int i = 0; i < itemCount && used < shuffled.Count; i++, used++)
    {
        Vector3 pos = GridToWorld(shuffled[used], offset);
        Instantiate(itemPrefabs[Random.Range(0, itemPrefabs.Length)], pos, Quaternion.identity);
    }

    // Traps
    for (int i = 0; i < trapCount && used < shuffled.Count; i++, used++)
    {
        Vector3 pos = GridToWorld(shuffled[used], offset);
        Instantiate(trapPrefabs[Random.Range(0, trapPrefabs.Length)], pos, Quaternion.identity);
    }

    // Ladder
    if (used < shuffled.Count)
{
    Vector3 pos = GridToWorld(shuffled[used], offset);
    GameObject ladder = Instantiate(ladderPrefab, pos, Quaternion.identity);

    Ladder ladderScript = ladder.GetComponent<Ladder>();
    if (ladderScript != null)
    {
        Image fadeOverlay = FindObjectOfType<Canvas>().GetComponentInChildren<Image>();
        ladderScript.Initialize(this, fadeOverlay);
    }
}

}

Vector3 GridToWorld(Vector2Int gridPos, float offset)
{
    return new Vector3(gridPos.x * cellSize - offset, 0f, gridPos.y * cellSize - offset);
}


}

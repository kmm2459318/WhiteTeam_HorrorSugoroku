using UnityEngine;

public class MapCreator : MonoBehaviour
{
    public GameObject BlocklTile;
    public GameObject NormalTile;
    public GameObject EventTile;
    public GameObject ItemTile;
    public GameObject DirecTile;
    public GameObject DebuffTile;
    public GameObject BatteryTile;

    private int[,] MapData =
    {
        {1,1,1,1,1,1,1,1,1,1,1 },
        {2,0,0,0,0,1,0,0,0,0,1 },
        {1,0,0,0,0,1,0,0,0,0,1 },
        {2,0,0,0,0,1,0,0,0,0,1 },
        {1,0,0,0,0,1,0,0,0,0,1 },
        {2,1,1,1,1,2,1,1,1,1,1 },
        {1,0,0,0,0,1,0,0,0,0,1 },
        {2,0,0,0,0,1,0,0,0,0,1 },
        {1,0,0,0,0,1,0,0,0,0,1 },
        {2,0,0,0,0,1,0,0,0,0,1 },
        {1,1,1,1,1,1,1,1,1,1,1 },
    };
    private void Start()
    {
        CreateMap();
    }

    void CreateMap()
    {
        for (int y = 0; y < MapData.GetLength(0); y++)
        {
            for (int x = 0; x < MapData.GetLength(1); x++)
            {
                GameObject tilePrefab = null;
                switch (MapData[y, x])
                {
                    case 0:
                        tilePrefab = BlocklTile;
                        break;
                    case 1:
                        tilePrefab = NormalTile;
                        break;
                    case 2:
                        tilePrefab = EventTile;
                        break;
                    case 3:
                        tilePrefab = ItemTile;
                        break;
                    case 4:
                        tilePrefab = DirecTile;
                        break;
                    case 5:
                        tilePrefab = DebuffTile;
                        break;
                    case 6:
                        tilePrefab = BatteryTile;
                        break;

                }
                if (tilePrefab != null)
                {
                    Instantiate(tilePrefab, new Vector3(2 * x, 0,2 * y), Quaternion.identity);
                }
            }
        }
    }
}

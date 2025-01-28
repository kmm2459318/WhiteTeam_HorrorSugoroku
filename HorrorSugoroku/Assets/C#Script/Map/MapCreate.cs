using UnityEngine;
using System.Collections.Generic;

public class MapCreator : MonoBehaviour
{
    public GameObject Wall;
    public GameObject NormalTile;
    public GameObject EventTile;
    public GameObject ItemTile;
    public GameObject DirecTile;
    public GameObject DebuffTile;
    public GameObject BatteryTile;
    public GameObject doorPrefab; // �h�A��Prefab��ǉ�

    private int[,] MapData =
    {
        {0,0,0,0,0,0,0,0,0,0,0,0,0 },
        {0,4,5,6,1,2,3,4,5,6,1,2,0 },
        {0,2,0,0,0,0,1,0,0,0,0,3,0 },
        {0,3,0,0,0,0,1,0,0,0,0,4,0 },
        {0,2,0,0,0,0,1,0,0,0,0,5,0 },
        {0,1,0,0,0,0,1,0,0,0,0,6,0 },
        {0,6,1,1,1,1,7,1,1,1,1,1,0 }, // �h�A��z�u����ʒu��7��ǉ�
        {0,5,0,0,0,0,1,0,0,0,0,2,0 },
        {0,4,0,0,0,0,1,0,0,0,0,3,0 },
        {0,3,0,0,0,0,1,0,0,0,0,4,0 },
        {0,2,0,0,0,0,1,0,0,0,0,5,0 },
        {0,1,4,3,2,6,5,4,3,2,1,6,0 },
        {0,0,0,0,0,0,0,0,0,0,0,0,0 },
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
                        tilePrefab = Wall;
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
                    case 7:
                        tilePrefab = doorPrefab; // �h�A��Prefab��ǉ�
                        break;
                }
                if (tilePrefab != null)
                {
                    Instantiate(tilePrefab, new Vector3(2 * (x - 1), 0, 2 * (y - 1)), Quaternion.identity);
                }
            }
        }
    }
    void CreateFixedDoor()
    {
        // �Œ�ʒu�Ƀh�A��z�u
        Vector3 doorPosition = new Vector3(2 * (6 - 1), 0, 2 * (6 - 1)); // �ǂ̈ʒu�Ɋ�Â��ăh�A�̈ʒu��ݒ�
        Quaternion doorRotation = Quaternion.Euler(0, -90, 0); // Y�������-90�x��]

        // ����������2.5�ɐݒ�
        doorPosition.y = 2.5f;

        Instantiate(doorPrefab, doorPosition, doorRotation);
    }
}
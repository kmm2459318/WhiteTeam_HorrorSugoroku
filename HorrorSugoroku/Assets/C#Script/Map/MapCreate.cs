using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapCreator : MonoBehaviour
{
    // �}�X��prefab
    public GameObject Wall;
    public GameObject NormalTile;
    public GameObject EventTile;
    public GameObject ItemTile;
    public GameObject DirecTile;
    public GameObject DebuffTile;
    public GameObject BatteryTile;
    public GameObject DoorTile;

�@�@// �}�b�v�̏����ʒu��ς���
    public Vector3 MapStartPosition = new Vector3(0, 0, 0);

    // �}�b�v�̔ԍ�
    public string MapName;

    private Dictionary<string, int[,]> MapData = new Dictionary<string, int[,]>(); // �`���ێ����鎫��


    private void Start()
    {
        // �}�b�v�f�[�^�����̉��ɋL��
        MapData.Add("Sample", new int[,] {
        { 0,0,0,0,0,0,0,0,0,0,0,0,0 },
        { 0,4,5,6,1,2,3,4,5,6,1,2,0 },
        { 0,3,0,0,0,0,1,0,0,0,0,3,0 },
        { 0,3,0,0,0,0,1,7,1,1,0,4,0 },
        { 0,3,0,0,0,0,1,0,0,0,0,5,0 },
        { 0,1,0,0,0,0,1,0,0,0,0,6,0 },
        { 0,6,1,1,1,1,2,1,1,1,1,1,0 },
        { 0,2,0,0,0,0,1,0,0,0,0,2,0 },
        { 0,2,0,0,0,0,1,0,0,0,0,3,0 },
        { 0,3,0,0,0,0,1,0,0,0,0,4,0 },
        { 0,2,0,0,0,0,1,0,0,0,0,5,0 },
        { 0,1,4,3,2,6,5,2,2,2,1,6,0 },
        { 0,0,0,0,0,0,0,0,0,0,0,0,0 },
        });

        MapData.Add("Area1", new int[,]{
        {0,0,0,0,0,0,0,0,0,0,0,0,0 },
        {0,4,5,6,1,2,3,4,5,6,1,2,0 },
        {0,3,0,0,0,0,1,0,0,0,0,3,0 },
        {0,3,0,0,0,0,1,7,1,1,0,4,0 },
        {0,3,0,0,0,0,1,0,0,0,0,5,0 },
        {0,1,0,0,0,0,1,0,0,0,0,6,0 },
        {0,6,1,1,1,1,2,1,1,1,1,1,0 },
        {0,2,0,0,0,0,1,0,0,0,0,2,0 },
        {0,2,0,0,0,0,1,0,0,0,0,3,0 },
        {0,3,0,0,0,0,1,0,0,0,0,4,0 },
        {0,2,0,0,0,0,1,0,0,0,0,5,0 },
        {0,1,4,3,2,6,5,2,2,2,1,6,0 },
        {0,0,0,0,0,0,0,0,0,0,0,0,0 },
        });


        // �����܂�

        // �}�b�v�����̏������s
        CreateMap(MapName);
    }
    public void ReplaceTile(Vector3 position)
    {
        GameObject newTile = NormalTile; // �u��������^�C���i�����ł� NormalTile�j
        Instantiate(newTile, position, Quaternion.identity);
    }
    void CreateMap(string MapName)
    {
        int[,] CreateMapData = MapData[MapName];

        // �c���̃T�C�Y�擾
        int rows = CreateMapData.GetLength(0);
        int cols = CreateMapData.GetLength(1);

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < cols; x++)
            {
                GameObject tilePrefab = null;
                switch (CreateMapData[y, x])
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
                        tilePrefab = DoorTile;
                        break;
                }
                if (tilePrefab != null)
                {
                    //Vector3 TilePositon = new Vector3(2 * (x - 1), 0, 2 * (y - 1)) + MapStartPosition;
                    //Instantiate(tilePrefab, TilePositon, Quaternion.identity);
                    Vector3 tilePosition = new Vector3(2 * (x - 1), 0, 2 * (y - 1)) + MapStartPosition;

                    // tilePrefab���C���X�^���X��
                    GameObject tileInstance = Instantiate(tilePrefab, tilePosition, Quaternion.identity);

                    // DoorTile �Ȃ�폜���ɒu�������鏈����ǉ�
                    if (CreateMapData[y, x] == 7)
                    {
                        DoorTileScript doorScript = tileInstance.AddComponent<DoorTileScript>();
                        doorScript.mapCreator = this;
                    }
                }
            }
        }
    }
}

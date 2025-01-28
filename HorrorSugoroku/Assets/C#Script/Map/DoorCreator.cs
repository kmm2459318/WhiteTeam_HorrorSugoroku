using UnityEngine;
using System.Collections.Generic;

public class DoorCreator : MonoBehaviour
{
    public GameObject Door; // ドアのモデル
    public Vector3 positionOffset = new Vector3(0, 1, 0.5f); // ドアの位置オフセット

    public void CreateRandomDoor(int[,] mapData)
    {
        // 壁の位置をリストに保存
        List<Vector3> wallPositions = new List<Vector3>();

        for (int y = 0; y < mapData.GetLength(0); y++)
        {
            for (int x = 0; x < mapData.GetLength(1); x++)
            {
                if (mapData[y, x] == 0) // 壁のタイル
                {
                    wallPositions.Add(new Vector3(2 * (x - 1), 0, 2 * (y - 1)));
                }
            }
        }

        // ランダムな壁の位置を選択
        if (wallPositions.Count > 0)
        {
            Vector3 randomWallPosition = wallPositions[Random.Range(0, wallPositions.Count)];

            // ドアを生成する位置と回転を決定
            Vector3 doorPosition = randomWallPosition;
            Quaternion doorRotation = Quaternion.identity; // 初期回転

            // 周囲のタイルを確認して位置と回転を調整
            int x = (int)((randomWallPosition.x / 2) + 1);
            int y = (int)((randomWallPosition.z / 2) + 1);

            if (x > 0 && mapData[y, x - 1] != 0) // 左側にタイルがある場合
            {
                doorPosition += new Vector3(-1, 0, 0);
                doorRotation = Quaternion.Euler(0, 90, 0); // Y軸を基準に90度回転
            }
            else if (x < mapData.GetLength(1) - 1 && mapData[y, x + 1] != 0) // 右側にタイルがある場合
            {
                doorPosition += new Vector3(1, 0, 0);
                doorRotation = Quaternion.Euler(0, -90, 0); // Y軸を基準に-90度回転
            }
            else if (y > 0 && mapData[y - 1, x] != 0) // 下側にタイルがある場合
            {
                doorPosition += new Vector3(0, 0, -1);
                doorRotation = Quaternion.Euler(0, 0, 0); // 回転なし
            }
            else if (y < mapData.GetLength(0) - 1 && mapData[y + 1, x] != 0) // 上側にタイルがある場合
            {
                doorPosition += new Vector3(0, 0, 1);
                doorRotation = Quaternion.Euler(0, 180, 0); // Y軸を基準に180度回転
            }

            // ドアを生成
            Instantiate(Door, doorPosition + positionOffset, doorRotation);
        }
    }
}
using UnityEngine;
using System.Collections.Generic;

public class DoorCreator : MonoBehaviour
{
    public GameObject Door; // �h�A�̃��f��
    public Vector3 positionOffset = new Vector3(0, 1, 0.5f); // �h�A�̈ʒu�I�t�Z�b�g

    public void CreateRandomDoor(int[,] mapData)
    {
        // �ǂ̈ʒu�����X�g�ɕۑ�
        List<Vector3> wallPositions = new List<Vector3>();

        for (int y = 0; y < mapData.GetLength(0); y++)
        {
            for (int x = 0; x < mapData.GetLength(1); x++)
            {
                if (mapData[y, x] == 0) // �ǂ̃^�C��
                {
                    wallPositions.Add(new Vector3(2 * (x - 1), 0, 2 * (y - 1)));
                }
            }
        }

        // �����_���ȕǂ̈ʒu��I��
        if (wallPositions.Count > 0)
        {
            Vector3 randomWallPosition = wallPositions[Random.Range(0, wallPositions.Count)];

            // �h�A�𐶐�����ʒu�Ɖ�]������
            Vector3 doorPosition = randomWallPosition;
            Quaternion doorRotation = Quaternion.identity; // ������]

            // ���͂̃^�C�����m�F���Ĉʒu�Ɖ�]�𒲐�
            int x = (int)((randomWallPosition.x / 2) + 1);
            int y = (int)((randomWallPosition.z / 2) + 1);

            if (x > 0 && mapData[y, x - 1] != 0) // �����Ƀ^�C��������ꍇ
            {
                doorPosition += new Vector3(-1, 0, 0);
                doorRotation = Quaternion.Euler(0, 90, 0); // Y�������90�x��]
            }
            else if (x < mapData.GetLength(1) - 1 && mapData[y, x + 1] != 0) // �E���Ƀ^�C��������ꍇ
            {
                doorPosition += new Vector3(1, 0, 0);
                doorRotation = Quaternion.Euler(0, -90, 0); // Y�������-90�x��]
            }
            else if (y > 0 && mapData[y - 1, x] != 0) // �����Ƀ^�C��������ꍇ
            {
                doorPosition += new Vector3(0, 0, -1);
                doorRotation = Quaternion.Euler(0, 0, 0); // ��]�Ȃ�
            }
            else if (y < mapData.GetLength(0) - 1 && mapData[y + 1, x] != 0) // �㑤�Ƀ^�C��������ꍇ
            {
                doorPosition += new Vector3(0, 0, 1);
                doorRotation = Quaternion.Euler(0, 180, 0); // Y�������180�x��]
            }

            // �h�A�𐶐�
            Instantiate(Door, doorPosition + positionOffset, doorRotation);
        }
    }
}
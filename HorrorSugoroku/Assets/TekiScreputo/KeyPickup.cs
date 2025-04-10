
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    [SerializeField] private GameObject keyPrefab; // ���̃v���n�u�iInspector�Ŏw��j
    [SerializeField] private List<Transform> spawnPoints; // ���̏o���|�C���g�i��̃I�u�W�F�N�g��o�^�j

    // ���̖��O���X�g
    private List<string> keyNames = new List<string>
    {
        "�H���̌�", "�n���̌�", "�z�[���̌�", "�x�b�g���[���̌�", "�㖱���̌�"
    };

    void Start()
    {
        SpawnKeys();
    }

    void SpawnKeys()
    {
        if (spawnPoints.Count < keyNames.Count)
        {
            Debug.LogError("�o���|�C���g������܂���B");
            return;
        }

        // �o���|�C���g���R�s�[���ăV���b�t��
        List<Transform> availablePoints = new List<Transform>(spawnPoints);
        ShuffleList(availablePoints);

        // ���������_���ɏo���|�C���g�֐���
        for (int i = 0; i < keyNames.Count; i++)
        {
            Vector3 position = availablePoints[i].position;
            GameObject key = Instantiate(keyPrefab, position, Quaternion.identity);
            key.name = keyNames[i];

            // ���ɖ��O��n���iKeyPickup�X�N���v�g�Ȃǂ�����ꍇ�j
            //KeyPickup pickup = key.GetComponent<KeyPickup>();
            //if (pickup != null)
            //{
            //    pickup.keyName = keyNames[i];
            //}
        }
    }

    // �V���b�t���֐�
    void ShuffleList<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            T temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}

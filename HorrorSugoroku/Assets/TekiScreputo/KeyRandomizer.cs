using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyRandomizer : MonoBehaviour
{
    public int totalItemCount = 10;           // �S�̂̊���U�萔
    public int fixedFirstFloorKeyCount = 3;   // ��K�̌��̐�

    private List<string> otherItems = new List<string> { "���l�`", "�񕜖�" };
    private List<string> generatedItems = new List<string>();

    void Awake()
    {
        GenerateItemList();
    }

    public void GenerateItemList()
    {
        generatedItems.Clear();

        // ��K�̌����m���ɒǉ�
        for (int i = 0; i < fixedFirstFloorKeyCount; i++)
        {
            generatedItems.Add("��K�̌�");
        }

        // �c��g�ɑ��A�C�e���������_���ɒǉ�
        int remaining = totalItemCount - fixedFirstFloorKeyCount;
        for (int i = 0; i < remaining; i++)
        {
            int rand = Random.Range(0, otherItems.Count);
            generatedItems.Add(otherItems[rand]);
        }

        Shuffle(generatedItems);
    }

    // �O���p�F�����ς݂̃��X�g��n��
    public List<string> GetGeneratedItems()
    {
        return new List<string>(generatedItems);
    }

    private void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int rand = Random.Range(i, list.Count);
            T temp = list[i];
            list[i] = list[rand];
            list[rand] = temp;
        }
    }
}

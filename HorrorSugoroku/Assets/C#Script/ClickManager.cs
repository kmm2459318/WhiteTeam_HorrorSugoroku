using UnityEngine;

public class ClickManager : MonoBehaviour
{
    void OnEnable()
    {
        //Outline.OnOutlineKeyPressed += HandleOutlineKeyPressed;
    }

    void OnDisable()
    {
        //Outline.OnOutlineKeyPressed -= HandleOutlineKeyPressed;
    }

    private void HandleOutlineKeyPressed()
    {
        // �v���C���[�C���x���g���ɃA�C�e����ǉ�
        PlayerInventory playerInventory = FindObjectOfType<PlayerInventory>();
        if (playerInventory != null)
        {
            // ���j�[�N��ID�𐶐��i��: ���O + ���ݎ����j
            string itemID = "��K�̃J�M_" + Time.time;

            playerInventory.AddItem("��K�̃J�M", itemID); // itemID ��n��
            Debug.Log($"��K�̃J�M����ɓ���܂����IID: {itemID}");
        }
        else
        {
            Debug.LogError("PlayerInventory ��������܂���I");
        }
    }
}
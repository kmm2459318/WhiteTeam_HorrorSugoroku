using UnityEngine;

public class ClickManager : MonoBehaviour
{
    void OnEnable()
    {
        Outline.OnOutlineKeyPressed += HandleOutlineKeyPressed;
    }

    void OnDisable()
    {
        Outline.OnOutlineKeyPressed -= HandleOutlineKeyPressed;
    }

    private void HandleOutlineKeyPressed()
    {
        // �v���C���[�C���x���g���ɃA�C�e����ǉ�
        PlayerInventory playerInventory = FindObjectOfType<PlayerInventory>();
        if (playerInventory != null)
        {
            playerInventory.AddItem("��K�̃J�M");
            Debug.Log("��K�̃J�M����ɓ���܂����I");
        }
        else
        {
            Debug.LogError("PlayerInventory ��������܂���I");
        }
    }
}
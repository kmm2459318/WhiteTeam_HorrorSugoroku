using UnityEngine;
using UnityEngine.UI;

public class ItemUIController : MonoBehaviour
{
    public GameObject itemObject; // �Q�[���I�u�W�F�N�g
    public GameObject backObjectUI; // �Q�[���I�u�W�F�N�g
    public Button itemButton; // �{�^��
    public Button backButton; // �{�^��

    void Start()
    {
        // �{�^���̃N���b�N�C�x���g�Ƀ��\�b�h��o�^
        itemButton.onClick.AddListener(ItemObject);
        backButton.onClick.AddListener(BackObject);
        itemObject.SetActive(false);
        backObjectUI.SetActive(false);
    }

    void ItemObject()
    {
            itemObject.SetActive(true);
            backObjectUI.SetActive(true);
    }

    void BackObject()
    {
            itemObject.SetActive(false);
            backObjectUI.SetActive(false);
    }
}

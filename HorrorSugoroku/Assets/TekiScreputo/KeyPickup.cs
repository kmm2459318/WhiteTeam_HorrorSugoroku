using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public string keyName = "";  // �E���L�[�̖��O
   // public float pickupCooldown = 5f;  // ���ɏE����܂ł̑ҋ@���ԁi�b�j

    private bool canPickup = true;  // �L�[���E�����Ԃ��ǂ���

    // PlayerInventory �X�N���v�g���Q��
    private PlayerInventory playerInventory;

    private void Start()
    {
        playerInventory = FindObjectOfType<PlayerInventory>(); // PlayerInventory ���擾
    }

    private void OnTriggerEnter(Collider other)
    {
        // �v���C���[���L�[�ɐG�ꂽ�Ƃ��ɃC���x���g���ɒǉ�
        if (other.CompareTag("Player") && canPickup)
        {
            PickupKey();
        }
    }

    // �L�[���E������
    private void PickupKey()
    {
        if (playerInventory != null)
        {
            playerInventory.AddItem(keyName); // �C���x���g���ɃL�[��ǉ�
            Debug.Log($"{keyName} ���E���܂����I");

           
        }
    }

   
}
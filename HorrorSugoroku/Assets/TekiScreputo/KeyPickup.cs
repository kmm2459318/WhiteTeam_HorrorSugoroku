using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public string keyName = "��"; // ���̖��O
    public AudioClip pickupSound; // �擾��
    private bool isCollected = false; // �擾�ς݂��ǂ���

    void OnMouseDown()
    {
        if (!isCollected)
        {
            CollectKey();
        }
    }

    void CollectKey()
    {
        isCollected = true;

        // �C���x���g���Ɍ���ǉ�
        PlayerInventory inventory = FindObjectOfType<PlayerInventory>();
        if (inventory != null)
        {
            inventory.AddItem(keyName);
        }

        // ���̎擾�����Đ�
        if (pickupSound != null)
        {
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);
        }

        // �����폜
        Destroy(gameObject);
    }
}

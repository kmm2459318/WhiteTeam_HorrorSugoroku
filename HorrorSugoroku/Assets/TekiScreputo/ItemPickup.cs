using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public string itemName = "��"; // �擾�ł���A�C�e����

    private void OnTriggerEnter(Collider other)
    {
        PlayerInventory inventory = other.GetComponent<PlayerInventory>();

        if (inventory != null)
        {
            inventory.AddItem(itemName); // �A�C�e�����擾
            inventory.ShowInventory(); // �C���x���g�������O�ɕ\��
             Destroy(gameObject); // �A�C�e��������
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

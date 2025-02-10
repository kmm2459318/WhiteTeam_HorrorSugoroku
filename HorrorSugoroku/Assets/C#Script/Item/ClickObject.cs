using UnityEngine;

public class ClickObject : MonoBehaviour
{
    public PlayerSaikoro playerSaikoro; // PlayerSaikoro�N���X�ւ̎Q��
    public PlayerInventory playerInventory; // PlayerInventory �ւ̎Q��
    public string itemName = "��"; // ���̖��O
    void Start()
    {
        // ������ `PlayerInventory` ���擾
        playerInventory = FindObjectOfType<PlayerInventory>();

        // `PlayerSaikoro` �������擾
        playerSaikoro = FindObjectOfType<PlayerSaikoro>();

        // Null�`�F�b�N
        if (playerInventory == null)
            Debug.LogError("PlayerInventory ��������܂���I�v���C���[�ɃA�^�b�`����Ă��܂����H");

        if (playerSaikoro == null)
            Debug.LogError("PlayerSaikoro ��������܂���I");
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // ���N���b�N
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) // ���C�L���X�g�ŃI�u�W�F�N�g�𔻒�
            {
                if (hit.collider.CompareTag("Object")) // �^�O�� "Object" �̏ꍇ
                {
                    // idoutyu��false�̂Ƃ��̂݃N���b�N�\
                    if (!playerSaikoro.idoutyu)
                    {
                        float distance = Vector3.Distance(Camera.main.transform.position, hit.collider.transform.position);

                        if (distance <= 3f) // �J��������̋�����3�ȉ��̏ꍇ
                        {
                            string itemName = hit.collider.gameObject.name; // �擾����A�C�e����
                            Debug.Log(itemName + " ����肵�܂���");

                          
                            // �C���x���g���� `null` �łȂ���Βǉ�
                            if (playerInventory != null)
                            {
                                playerInventory.AddItem(itemName);
                            }
                            else
                            {
                                Debug.LogError("playerInventory ���ݒ肳��Ă��܂���I");
                            }
                            // �N���b�N�����I�u�W�F�N�g���폜
                            Destroy(hit.collider.gameObject);
                        }
                    }
                }
            }
        }
    }
}

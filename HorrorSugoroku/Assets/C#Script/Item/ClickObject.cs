using UnityEngine;

public class ClickObject : MonoBehaviour
{
    public PlayerSaikoro playerSaikoro; // PlayerSaikoro�N���X�ւ̎Q��

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
                            Debug.Log(hit.collider.gameObject.name + " ���N���b�N����܂���");

                            // �N���b�N�����I�u�W�F�N�g���폜
                            Destroy(hit.collider.gameObject);
                        }
                    }
                }
            }
        }
    }
}

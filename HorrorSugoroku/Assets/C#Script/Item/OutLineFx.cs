using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/*�@Outline.cs�𓯂��I�u�W�F�N�g�ɃA�^�b�`���ĂȂ��Ɠ����Ȃ��@*/

public class OutLineFx : MonoBehaviour
{
    public PlayerSaikoro playerSaikoro;

    private GameObject lastHighlightedObject = null;

    void Start()
    {
        // �V�[�����̂��ׂĂ� "Item", "Key", "Doll"�^�O�����I�u�W�F�N�g�̃A�E�g���C�����ŏ���OFF�ɂ���
        string[] tags = { "Item", "Key", "Doll" };

        foreach (string tag in tags)
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
            foreach (GameObject obj in objects)
            {
                Outline outline = obj.GetComponent<Outline>();
                if (outline != null)
                {
                    outline.enabled = false;
                }
            }
        }
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // �^�O�� "Item" "Key" "Map"�̏ꍇ
            if (hit.collider.CompareTag("Item") || hit.collider.CompareTag("Key") || hit.collider.CompareTag("Doll"))
            {
                //�v���C���[���ړ��������Ă�����
                if (!playerSaikoro.idoutyu)
                {
                    Debug.Log("�ړ��������G��Ă���");
                    if (IsLookingAtObject(hit.collider.gameObject)) // **�����̕����ɂ��邩�m�F**
                    {
                        float distance = Vector3.Distance(Camera.main.transform.position, hit.collider.transform.position);

                        if (distance <= 3f) // �J��������̋�����3�ȉ��̏ꍇ
                        {
                            if (lastHighlightedObject != hit.collider.gameObject)
                            {
                                if (lastHighlightedObject != null)
                                {
                                    //�G�t�F�N�g��OFF�ɂ���
                                    lastHighlightedObject.GetComponent<Outline>().enabled = false;
                                }

                                lastHighlightedObject = hit.collider.gameObject;
                                //�G�t�F�N�g��ON�ɂ���
                                lastHighlightedObject.GetComponent<Outline>().enabled = true;

                            }
                            return;
                        }
                    }
                }
            }
        }

        if (lastHighlightedObject != null)
        {
            lastHighlightedObject.GetComponent<Outline>().enabled = false;
            lastHighlightedObject = null;
        }
    }

    bool IsLookingAtObject(GameObject obj)
    {
        Vector3 directionToObject = (obj.transform.position - Camera.main.transform.position).normalized;
        float dotProduct = Vector3.Dot(Camera.main.transform.forward, directionToObject);

        return dotProduct > 0.8f; // **0.8�ȏ�Ȃ�v���C���[�̎��������ɂ���**
    }
}

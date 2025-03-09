using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Outline.cs�𓯂��I�u�W�F�N�g�ɃA�^�b�`���ĂȂ��Ɠ����Ȃ�*/
public class OutLineFx : MonoBehaviour
{
    public PlayerSaikoro playerSaikoro;

    private GameObject lastHighlightedObject = null;

    void Start()
    {
        //// �V�[�����̂��ׂĂ� "Item", "Key", "Map"�^�O�����I�u�W�F�N�g�̃A�E�g���C�����ŏ���OFF�ɂ���
        //string[] tags = { "Item", "Key", "Map" };

        //foreach (string tag in tags)
        //{
        //    GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
        //    foreach (GameObject obj in objects)
        //    {
        //        Outline outline = obj.GetComponent<Outline>();
        //        if (outline != null)
        //        {
        //            outline.enabled = false;
        //        }
        //    }
        //}

        // �V�[�����̂��ׂĂ� "Item" �^�O�����I�u�W�F�N�g�̃A�E�g���C�����ŏ���OFF�ɂ���
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Item");
        foreach (GameObject obj in objects)
        {
            Outline outline = obj.GetComponent<Outline>();
            if (outline != null)
            {
                outline.enabled = false;
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
            if (hit.collider.CompareTag("Item"))
            {
                //�v���C���[���ړ��������Ă�����
                if (!playerSaikoro.idoutyu)
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

        if (lastHighlightedObject != null)
        {
            lastHighlightedObject.GetComponent<Outline>().enabled = false;
            lastHighlightedObject = null;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Outline.csを同じオブジェクトにアタッチしてないと動かない*/
public class OutLineFx : MonoBehaviour
{
    public PlayerSaikoro playerSaikoro;

    private GameObject lastHighlightedObject = null;

    void Start()
    {
        //// シーン内のすべての "Item", "Key", "Map"タグを持つオブジェクトのアウトラインを最初にOFFにする
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

        // シーン内のすべての "Item" タグを持つオブジェクトのアウトラインを最初にOFFにする
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
            // タグが "Item" "Key" "Map"の場合
            if (hit.collider.CompareTag("Item"))
            {
                //プレイヤーが移動完了していたら
                if (!playerSaikoro.idoutyu)
                {
                    if (lastHighlightedObject != hit.collider.gameObject)
                    {
                        if (lastHighlightedObject != null)
                        {
                            //エフェクトをOFFにする
                            lastHighlightedObject.GetComponent<Outline>().enabled = false;
                        }

                        lastHighlightedObject = hit.collider.gameObject;
                        //エフェクトをONにする
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

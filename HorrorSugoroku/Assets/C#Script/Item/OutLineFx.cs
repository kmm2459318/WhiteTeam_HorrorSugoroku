using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/*　Outline.csを同じオブジェクトにアタッチしてないと動かない　*/

public class OutLineFx : MonoBehaviour
{
    public PlayerSaikoro playerSaikoro;

    private GameObject lastHighlightedObject = null;

    void Start()
    {
        // シーン内のすべての "Item", "Key", "Doll"タグを持つオブジェクトのアウトラインを最初にOFFにする
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
            // タグが "Item" "Key" "Map"の場合
            if (hit.collider.CompareTag("Item") || hit.collider.CompareTag("Key") || hit.collider.CompareTag("Doll"))
            {
                //プレイヤーが移動完了していたら
                if (!playerSaikoro.idoutyu)
                {
                    Debug.Log("移動完了＆触れている");
                    if (IsLookingAtObject(hit.collider.gameObject)) // **視線の方向にあるか確認**
                    {
                        float distance = Vector3.Distance(Camera.main.transform.position, hit.collider.transform.position);

                        if (distance <= 3f) // カメラからの距離が3以下の場合
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

        return dotProduct > 0.8f; // **0.8以上ならプレイヤーの視線方向にある**
    }
}

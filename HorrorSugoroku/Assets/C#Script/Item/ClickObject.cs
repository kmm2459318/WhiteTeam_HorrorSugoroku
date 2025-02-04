using UnityEngine;

public class ClickObject : MonoBehaviour
{
    public PlayerSaikoro playerSaikoro; // PlayerSaikoroクラスへの参照

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 左クリック
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) // レイキャストでオブジェクトを判定
            {
                if (hit.collider.CompareTag("Object")) // タグが "Object" の場合
                {
                    // idoutyuがfalseのときのみクリック可能
                    if (!playerSaikoro.idoutyu)
                    {
                        float distance = Vector3.Distance(Camera.main.transform.position, hit.collider.transform.position);

                        if (distance <= 3f) // カメラからの距離が3以下の場合
                        {
                            Debug.Log(hit.collider.gameObject.name + " がクリックされました");

                            // クリックしたオブジェクトを削除
                            Destroy(hit.collider.gameObject);
                        }
                    }
                }
            }
        }
    }
}

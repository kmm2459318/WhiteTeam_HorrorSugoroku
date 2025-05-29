using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngineInternal;

public class Statue : MonoBehaviour
{
    public GameManager gameManager;
    public PlayerSaikoro playerSaikoro;

    //人形を置くエリア
    public GameObject[] PutDownArea = new GameObject[4];
    //人形用の箱
    public GameObject[] Doll = new GameObject[4];
    //ライト
    public Light Clearlight;
    public int PutDoll; //人形を置いた数
    private float AddIntensity = 1000f; //光の強さの増加量
    public float WaitTime = 7f; // 待ち時間（秒）

    private float Timer = 0f; // タイマー

    private GameObject lastHighlightedObject = null;

    private bool hasClicked = false;
    public bool isExitDoor = false; // 脱出ドアであることがわかるタグ
    private bool sceneLoaded = false; //二重読み込み防止フラグ

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 人形を非表示
        for (int i = 0; i < Doll.Length; i++)
        {
            if (Doll[i] != null)
            {
                Doll[i].SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !hasClicked)
        {
            hasClicked = true;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                lastHighlightedObject = hit.collider.gameObject;
                // タグが "DollArea"の場合
                if (hit.collider.CompareTag("DollArea"))
                {
                    //プレイヤーが移動完了していたら
                    if (!playerSaikoro.idoutyu && !gameManager.isPlayerTurn)
                    {
                        Debug.Log("移動完了＆触れている");
                        if (IsLookingAtObject(hit.collider.gameObject))
                        {
                            //人形を持っている数を確認
                            if (gameManager.CanPlaceDoll())
                            {
                                for (int i = 0; i < PutDownArea.Length; i++)
                                {
                                    if (PutDownArea[i] == hit.collider.gameObject) // どのエリアをクリックしたか判定
                                    {
                                        if (Doll[i] != null && !Doll[i].activeSelf)
                                        {
                                            Doll[i].SetActive(true); // 対応する人形のみ表示
                                            PutDoll++; // 人形を置いた数をカウント
                                            gameManager.PlaceDoll();

                                            CheckAllDoll(); //人形を全部置いたかの判定
                                        }
                                    }
                                }
                                lastHighlightedObject.GetComponent<Outline>().enabled = false;
                            }
                            else
                            {
                                Debug.Log("こ↑こ↓になにかを置けそうだ");
                            }
                        }
                    }
                }
            }

            StartCoroutine(ResetClick()); // フラグリセットコルーチン呼び出し
        }

        //脱出可能になったら光を強くする
        if (isExitDoor && !sceneLoaded)
        {
            Debug.Log("脱出演出");
            Clearlight.intensity += AddIntensity * Time.deltaTime;
            // タイマーを進める
            Timer += Time.deltaTime;

            //一定の時間を超えたらシーン読み込み
            if (Timer >= WaitTime)
            {
                sceneLoaded = true; // 二重読み込み防止
                GameState.IsGameClear = true;
                SceneManager.LoadScene("Ending");
            }
        }

        
    }

    IEnumerator ResetClick()
    {
        yield return new WaitForSeconds(0.1f); // 多重クリック防止時間
        hasClicked = false;
    }

    bool IsLookingAtObject(GameObject obj)
    {
        Vector3 directionToObject = (obj.transform.position - Camera.main.transform.position).normalized;
        float dotProduct = Vector3.Dot(Camera.main.transform.forward, directionToObject);

        return dotProduct > 0.8f; // **0.8以上ならプレイヤーの視線方向にある**
    }

    //人形を全部置いたかの判定
    void CheckAllDoll()
    {
        for(int i =0; i < Doll.Length; i++)
        {
            if (!Doll[i].active)
            {
                return;
            }
        }

        isExitDoor = true;

    }

}

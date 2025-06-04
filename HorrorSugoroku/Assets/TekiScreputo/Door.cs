using System.Collections;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    public Animator doorAnimator; // ドアのアニメーター
    public Animator doorAnimatorLeft;
    public float interactionRange = 3f;
    private bool isOpen = false; // ドアの状態
    private bool isUnlocked = false; // ドアが一度開かれたかどうか
    public Animator childAnimator; // 子オブジェクトのアニメーター
    public Animator rightAnimator; // 右側のドアのアニメーター
    public bool requiresKey = true; // デフォルトは「鍵が必要」
    private Transform player; // プレイヤーの Transform
    public PlayerInventory playerInventory; // プレイヤーのインベントリ参照
    public string requiredItem; // 必要なアイテム（インスペクターで指定）

    public GameObject doorPanel; // UIのパネル
    public TextMeshProUGUI doorText; // UIのテキスト
    public float messageDisplayTime = 2f; // メッセージを表示する時間（秒）

    public GameObject hiddenArea; // 表示したいマス（ドアが開くと表示）
    public GameObject targetObject; // 消したい対象
    public Transform windowTransform1; // 窓1のTransform
    public Transform windowTransform2; // 窓2のTransform

    public EnemyAppearance enemyAppearance; // エネミーの表示制御
    public GameObject requiredTileObject; //プレイヤーが止まるべきマスのオブジェクト
    public AudioSource audioSource; //オーディオソースを参照
    public AudioClip doorOpenSpund; //ドアが開く音
    public AudioClip keyMissingSound; //カギがないときの音
    public AudioClip[] scareSound; //脅かしの音
    Coroutine messageCoroutine;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (player != null)
        {
            //Debug.Log("プレイヤーオブジェクトが見つかりました。");
            playerInventory = player.GetComponent<PlayerInventory>();

            //if (playerInventory != null)
            //{
            //    Debug.Log("PlayerInventoryが正しく取得されました。");
            //}
            //else
            //{
            //    Debug.LogError("PlayerInventoryが見つかりません。");
            //}
        }
        //else
        //{
        //    Debug.LogError("プレイヤーオブジェクトが見つかりません。");
        //}
       
        if (doorPanel != null)
        {
            doorPanel.SetActive(false); // 最初はUIを非表示
        }

        if (hiddenArea != null)
        {
            hiddenArea.SetActive(false); // 最初は隠れたエリアを非表示
        }

        // 左側のドアのAnimatorを名前で取得
        Transform leftTransform = transform.Find("Left");
        if (leftTransform != null)
        {
            childAnimator = leftTransform.GetComponent<Animator>();
            //Debug.Log("左側のアニメーターが設定されました");

            // 窓1のTransformを取得
            windowTransform1 = leftTransform.Find("Window1");
            //if (windowTransform1 != null)
            //{
            //    Debug.Log("窓1のTransformが設定されました");
            //}
            //else
            //{
            //    Debug.Log("窓1のオブジェクトが見つかりません");
            //}

            // 窓2のTransformを取得
            windowTransform2 = leftTransform.Find("Window2");
            //if (windowTransform2 != null)
            //{
            //    Debug.Log("窓2のTransformが設定されました");
            //}
            //else
            //{
            //    Debug.Log("窓2のオブジェクトが見つかりません");
            //}
        }
        //else
        //{
        //    Debug.Log("左側のオブジェクトが見つかりません");
        //}

        // 右側のドアのAnimatorを名前で取得
        Transform rightTransform = transform.Find("Right");
        if (rightTransform != null)
        {
            rightAnimator = rightTransform.GetComponent<Animator>();
            //Debug.Log("右側のアニメーターが設定されました");
        }
        //else
        //{
        //    Debug.Log("右側のオブジェクトが見つかりません");
        //}

        // EnemyAppearanceの参照を取得
        enemyAppearance = GetComponent<EnemyAppearance>();
        //if (enemyAppearance == null)
        //{
        //    Debug.LogError("DoorオブジェクトにEnemyAppearanceコンポーネントが見つかりません");
        //}

        //AudioDourceコンポーネントを取得する
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        // 左クリックされたかチェック
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Raycast して何かに当たったか確認
            if (Physics.Raycast(ray, out hit))
            {
                // 当たったオブジェクトがこのドアか？
                if (hit.transform == this.transform || hit.transform.IsChildOf(this.transform))
                {
                    float distance = Vector3.Distance(player.position, transform.position);
                    if (distance <= interactionRange)
                    {
                        Debug.Log("ドアがクリックされました");

                        if (!isUnlocked)
                        {
                            if (!requiresKey || (playerInventory != null && playerInventory.HasItem(requiredItem)))
                            {
                                OpenDoorConfirmed(); // 鍵を使ってドアを開く
                            }
                            else
                            {
                                string itemName = string.IsNullOrEmpty(requiredItem) ? "鍵" : requiredItem;
                                string message = $"「{itemName}」がありません";
                                Debug.Log(message);
                                PlayKeyMissingSound(); //音を再生
                                ShowMessage(message); // ← UIで表示
                                return;
                            }
                        }

                        if (childAnimator != null)
                        {
                            Debug.Log("LeftOpenTriggerを設定");
                            childAnimator.SetTrigger("LeftOpenTrigger");
                            StartCoroutine(TransitionLeftAnimation());
                        }

                        if (rightAnimator != null)
                        {
                            Debug.Log("RightOpenTriggerを設定");
                            rightAnimator.SetTrigger("RightOpenTrigger");
                            StartCoroutine(TransitionRightAnimation());
                        }
                    }
                }
            }
        }
    }

    IEnumerator TransitionLeftAnimation()
    {
        yield return new WaitForSeconds(10f);
        if (childAnimator != null)
        {
            //Debug.Log("右2に開くドアを作成するアニメーションに移行");
            childAnimator.SetTrigger("CreateOpenDoorRight2");
        }
    }

    IEnumerator TransitionRightAnimation()
    {
        yield return new WaitForSeconds(10f);
        if (rightAnimator != null)
        {
            //Debug.Log("ドア2を作成するアニメーションに移行");
            rightAnimator.SetTrigger("CreateOpenDoor2");
        }
    }

    void LateUpdate()
    {
        // 左側のドアに追従する窓の位置や回転を更新
        if (windowTransform1 != null && childAnimator != null)
        {
            Transform leftDoorTransform = childAnimator.transform;
            windowTransform1.position = leftDoorTransform.position;
            windowTransform1.rotation = leftDoorTransform.rotation;
        }

        if (windowTransform2 != null && childAnimator != null)
        {
            Transform leftDoorTransform = childAnimator.transform;
            windowTransform2.position = leftDoorTransform.position;
            windowTransform2.rotation = leftDoorTransform.rotation;
        }

        // 右側のドアに追従する窓の位置や回転を更新
        if (windowTransform1 != null && rightAnimator != null)
        {
            Transform rightDoorTransform = rightAnimator.transform;
            windowTransform1.position = rightDoorTransform.position;
            windowTransform1.rotation = rightDoorTransform.rotation;
        }

        if (windowTransform2 != null && rightAnimator != null)
        {
            Transform rightDoorTransform = rightAnimator.transform;
            windowTransform2.position = rightDoorTransform.position;
            windowTransform2.rotation = rightDoorTransform.rotation;
        }
    }

    // ドアを開けるメソッド
    void OpenDoorConfirmed()
    {
        OpenDoor(); // ドアを開く
        playerInventory.UseItem(requiredItem); // 鍵をインベントリから削除
        isUnlocked = true; // ドアが開いたことを記録
        if (targetObject != null)
        {
            targetObject.SetActive(false);
        }

    }

    void ShowMessage(string message)
    {
        if (messageCoroutine != null)
        {
            StopCoroutine(messageCoroutine);
        }
        messageCoroutine = StartCoroutine(ShowMessageRoutine(message));
    }
    IEnumerator ShowMessageRoutine(string message)
    {
        if (doorPanel != null && doorText != null)
        {
            doorPanel.SetActive(true);
            doorText.text = message;
            yield return new WaitForSeconds(messageDisplayTime);
            doorPanel.SetActive(false);
        }
    }


    void OpenDoor()
    {
        if (doorAnimator != null)
        {
            doorAnimator.SetTrigger("LeftOpenTrigger");
        }
        if (doorAnimator != null)
        {
            doorAnimator.SetTrigger("RightOpenTrigger");
        }
        //ドアが開く音を再生する
        if (audioSource != null && doorOpenSpund != null)
        {
            audioSource.PlayOneShot(doorOpenSpund);
        }
        //20%の確率で脅かしの音声を再生する
        PlayScareSoundWithChance(0.2f);
        //Debug.Log($"{requiredItem} のドアが開きました");

        if (hiddenArea != null)
        {
            hiddenArea.SetActive(true);
        }

        // エネミーを非表示にする
        if (enemyAppearance != null)
        {
            Debug.Log("エネミーを非表示にします");
            enemyAppearance.HideEnemyAfterDelay(); // エネミーを非表示にするメソッドを呼び出す
        }
        else
        {
            //Debug.Log("enemyAppearanceがnullです");
        }
    }
    void PlayKeyMissingSound()
    {
        if (audioSource != null && keyMissingSound != null)
        {
            audioSource.PlayOneShot(keyMissingSound); //再生
        }
    }
    void PlayScareSoundWithChance(float chance)
    {
        if (audioSource != null && scareSound.Length>0)
        {
            //ランダム値を生成
            float randomValue = Random.Range(0f, 1f);
            if (randomValue <= chance)
            {
                //ランダムで音声を選択
                int randomIndex = Random.Range(0,scareSound.Length);
                AudioClip selectedSound = scareSound[randomIndex];
                Debug.Log($"脅かし音声{randomIndex}が再生されます！");
                audioSource.PlayOneShot(selectedSound);
            }
            else
            {
                Debug.Log("脅かしの音声は再生されません");
            }
            }
        }
    
}
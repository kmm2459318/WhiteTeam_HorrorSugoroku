using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerSaikoro : MonoBehaviour
{
    public GameManager gameManager; // GameManagerへの参照
    int movesum;
    private EnemySaikoro targetScript; // コマンドを受け取るEnemySaikoro
    private int sai = 1; // ランダムなサイコロの値
    private bool saikorotyu = false; // サイコロを振っているか
    private bool idoutyu = false;
    private int detame = 0;
    private bool PN = false;
    private bool PW = false;
    private bool PE = false;
    private bool PS = false;
    public Sprite s1;
    public Sprite s2;
    public Sprite s3;
    public Sprite s4;
    public Sprite s5;
    public Sprite s6;
    public GameObject saikoro;
    public GameObject Player;
    public GameObject PNorth;
    public GameObject PWest;
    public GameObject PEast;
    public GameObject PSouth;
    private Image image;

    [System.Obsolete]
    void Start()
    {
        // プレイヤーシーンがロードされる際に、EnemySaikoroを探して参照を保持
        targetScript = FindObjectOfType<EnemySaikoro>();

        PlayerPrefs.SetInt("move", 0);
        

        // サイコロのImageを保持
        image = saikoro.GetComponent<Image>();

        saikoro.SetActive(false);

        // Enemyがシーンに存在しない場合、エラーメッセージを出力
        if (targetScript == null)
        {
            Debug.Log("Enemyが無いよ");
        }
        if (saikoro != null)
        {
            image = saikoro.GetComponent<Image>();
            saikoro.SetActive(false);
        }
        else
        {
            Debug.LogError("Saikoro GameObject is not assigned in the Inspector.");
        }

        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
            if (gameManager == null)
            {
                Debug.LogError("GameManager is not assigned and could not be found in the scene.");
            }
        }
    }

    void Update()
    {
        if (!gameManager.IsPlayerTurn())
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.E) && !saikorotyu && !idoutyu)
        {
            saikorotyu = true;
            StartCoroutine(RollDice());
            this.delta += Time.deltaTime;

            if (this.delta > 0.1f)
            {
                this.delta = 0f;

                if (ii < 7)
                {
                    sai = Random.Range(1, 7);
                    saikoro.SetActive(true);
                    Debug.Log("Player rolling: " + sai);
                    ii++;
                }
                else
                {
                    sai = Random.Range(1, 7);
                    detame = sai;
                    Debug.Log("Player rolled: " + sai);

                    // プレイヤーのサイコロの結果に応じてEnemyのサイコロ範囲を決定
                    targetScript.RollEnemyDice(sai);

                    movesum += sai;
                    PlayerPrefs.SetInt("move", movesum);

                    ii = 0;
                    saikorotyu = false;
                    idoutyu = true;
                }
            }
        }

        if (idoutyu)
        {
            Vector3 Pos = Player.transform.position;
            PN = PNorth.GetComponent<PlayerNSEWCheck>().masuCheck;
            PW = PWest.GetComponent<PlayerNSEWCheck>().masuCheck;
            PE = PEast.GetComponent<PlayerNSEWCheck>().masuCheck;
            PS = PSouth.GetComponent<PlayerNSEWCheck>().masuCheck;

            if (Input.GetKeyDown(KeyCode.W) && PN)
            {
                Pos.z += 2.0f; // 北方向に2.0f移動
                Player.transform.position = Pos;
                sai--;
            }
            if (Input.GetKeyDown(KeyCode.A) && PW)
            {
                Pos.x -= 2.0f; // 西方向に2.0f移動
                Player.transform.position = Pos;
                sai--;
            }
            if (Input.GetKeyDown(KeyCode.S) && PS)
            {
                Pos.z -= 2.0f; // 南方向に2.0f移動
                Player.transform.position = Pos;
                sai--;
            }
            if (Input.GetKeyDown(KeyCode.D) && PE)
            {
                Pos.x += 2.0f; // 東方向に2.0f移動
                Player.transform.position = Pos;
                sai--;
            }
            if (sai < 1)
            {
                idoutyu = false;
                saikoro.SetActive(false);
                gameManager.NextTurn();
            }
        }
    }

    private IEnumerator RollDice()
    {
        saikoro.SetActive(true);
        for (int i = 0; i < 10; i++) // 10回ランダムに目を表示
        {
            sai = Random.Range(1, 7);
            switch (sai)
            {
                case 1:
                    image.sprite = s1; break;
                case 2:
                    image.sprite = s2; break;
                case 3:
                    image.sprite = s3; break;
                case 4:
                    image.sprite = s4; break;
                case 5:
                    image.sprite = s5; break;
                case 6:
                    image.sprite = s6; break;
            }
            yield return new WaitForSeconds(0.1f); // 0.1秒ごとに目を変更
        }

        detame = sai;
        Debug.Log("Player rolled: " + sai);

        saikorotyu = false;
        idoutyu = true;
    }

    public void StartRolling()
    {
        // このメソッドは空にしておくか、必要に応じて他の処理を追加します
    }
}
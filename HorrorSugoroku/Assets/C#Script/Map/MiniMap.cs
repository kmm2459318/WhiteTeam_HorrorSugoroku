using UnityEngine;

public class MiniMap : MonoBehaviour
{
    public GameManager gameManager;

    // ミニマップのオブジェクト
    public GameObject[] Map = new GameObject[9];

    // 地図の画像を格納
    public Sprite[] Sprites = new Sprite[9];

    public GameObject currentEnemyModel; // 現在のエネミーモデル
    public GameObject newEnemyModel; // 新しいエネミーモデル

    int Mapcount = 0;

    // StartはMonoBehaviourが作成された後、最初のUpdateの前に一度だけ呼び出される
    void Start()
    {
        // ミニマップを非表示
        for (int i = 0; i < Map.Length; i++)
        {
            if (Map[i] != null)
            {
                Map[i].SetActive(false);
            }
        }

        // デバッグログを追加して、currentEnemyModelとnewEnemyModelが正しく設定されているかを確認
        if (currentEnemyModel == null)
        {
            Debug.LogError("currentEnemyModel is not assigned.");
        }
        if (newEnemyModel == null)
        {
            Debug.LogError("newEnemyModel is not assigned.");
        }
    }

    // Updateはフレームごとに一度呼び出される
    void Update()
    {
        // マップの取得数を代入
        Mapcount = gameManager.mapPiece;

        // カウントが増えるごとに画像を変更し、表示する
        if (Mapcount >= 0 && Mapcount < Map.Length)
        {
            if (Map[Mapcount] != null)
            {
                Map[Mapcount].SetActive(true); // 表示
                SpriteRenderer sr = Map[Mapcount].GetComponent<SpriteRenderer>();

                if (sr != null && Sprites[Mapcount] != null)
                {
                    sr.sprite = Sprites[Mapcount]; // 画像を変更
                }
            }
        }

        // マップのピースが3枚手に入ったらエネミーモデルを変更
        if (Mapcount == 3)
        {
            ChangeEnemyModel();
        }
    }

    private void ChangeEnemyModel()
    {
        if (currentEnemyModel != null && newEnemyModel != null)
        {
            // 新しいエネミーモデルを元のエネミーモデルの位置に移動
            newEnemyModel.transform.position = currentEnemyModel.transform.position;
            newEnemyModel.transform.rotation = currentEnemyModel.transform.rotation;

            // 元のエネミーモデルを非アクティブにする
            currentEnemyModel.SetActive(false);

            // 新しいエネミーモデルをアクティブにする
            newEnemyModel.SetActive(true);

            // 新しいエネミーモデルにエネミーの仕様を適用
            EnemySaikoro enemySaikoro = newEnemyModel.GetComponent<EnemySaikoro>();
            if (gameManager.EnemyCopyOn)
            {
                EnemySaikoro enemyCopySaikoro = newEnemyModel.GetComponent<EnemySaikoro>();
            }

            // currentEnemyModelを新しいエネミーモデルに更新
            currentEnemyModel = newEnemyModel;
        }
        else
        {
            Debug.LogError("currentEnemyModel or newEnemyModel is not assigned.");
        }
    }
}
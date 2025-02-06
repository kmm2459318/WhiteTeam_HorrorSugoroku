using UnityEngine;

public class MiniMap : MonoBehaviour
{
    public GameManager gameManager;

    //ミニマップのオブジェクト
    public GameObject[] Map = new GameObject[9];

    //地図の画像を格納
    public Sprite[] Sprites = new Sprite[9];

    int Mapcount = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //ミニマップを非表示
        for (int i = 0; i < Map.Length; i++) {
            if (Map[i] != null)
            {
                Map[i].SetActive(false);
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        //マップの取得数を代入
        Mapcount = gameManager.mapPiece;

        //カウントが増えるごとに画像を変更し、表示する
        if(Mapcount >= 0 && Mapcount < Map.Length)
        {
            if (Map[Mapcount] != null)
            {
                Map[Mapcount].SetActive(true);//表示
                SpriteRenderer sr = Map[Mapcount].GetComponent<SpriteRenderer>();

                if(sr != null && Sprites[Mapcount] != null)
                {
                    sr.sprite = Sprites[Mapcount];//画像を変更
                }
            }
        }

    }
}

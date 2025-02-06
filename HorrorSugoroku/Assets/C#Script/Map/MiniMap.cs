using UnityEngine;

public class MiniMap : MonoBehaviour
{
    public GameManager gameManager;

    //�~�j�}�b�v�̃I�u�W�F�N�g
    public GameObject[] Map = new GameObject[9];

    //�n�}�̉摜���i�[
    public Sprite[] Sprites = new Sprite[9];

    int Mapcount = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //�~�j�}�b�v���\��
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
        //�}�b�v�̎擾������
        Mapcount = gameManager.mapPiece;

        //�J�E���g�������邲�Ƃɉ摜��ύX���A�\������
        if(Mapcount >= 0 && Mapcount < Map.Length)
        {
            if (Map[Mapcount] != null)
            {
                Map[Mapcount].SetActive(true);//�\��
                SpriteRenderer sr = Map[Mapcount].GetComponent<SpriteRenderer>();

                if(sr != null && Sprites[Mapcount] != null)
                {
                    sr.sprite = Sprites[Mapcount];//�摜��ύX
                }
            }
        }

    }
}

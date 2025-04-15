using UnityEngine;

public class MiniMap : MonoBehaviour
{
    public GameManager gameManager;

    // �~�j�}�b�v�̃I�u�W�F�N�g
    public GameObject[] Map = new GameObject[9];

    // �n�}�̉摜���i�[
    public Sprite[] Sprites = new Sprite[9];

    int Mapcount = 0;

    void Start()
    {
        // �~�j�}�b�v���\��
        for (int i = 0; i < Map.Length; i++)
        {
            if (Map[i] != null)
            {
                Map[i].SetActive(false);
            }
        }

        Map[0].SetActive(true); // �\��
        SpriteRenderer sr = Map[0].GetComponent<SpriteRenderer>();

        if (sr != null && Sprites[0] != null)
        {
            sr.sprite = Sprites[0]; // �摜��ύX
        }

    }

    public void UpdateMiniMap()
    {
        // �}�b�v�̎擾������
        Mapcount = gameManager.mapPiece;

        // �J�E���g�������邲�Ƃɉ摜��ύX���A�\������
        if (Mapcount >= 0 && Mapcount < Map.Length)
        {
            if (Map[Mapcount] != null)
            {
                Map[Mapcount].SetActive(true); // �\��
                SpriteRenderer sr = Map[Mapcount].GetComponent<SpriteRenderer>();

                if (sr != null && Sprites[Mapcount] != null)
                {
                    sr.sprite = Sprites[Mapcount]; // �摜��ύX
                }
            }
        }

        // �}�b�v�̃s�[�X��3����ɓ�������G�l�~�[���f����ύX
        if (Mapcount == 3 || Mapcount == 6)
        {
            gameManager.ChangeEnemyModel(Mapcount); // ������n���ă��\�b�h���Ăяo��
        }
    }
}
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    public GameManager gameManager;

    // �~�j�}�b�v�̃I�u�W�F�N�g
    public GameObject[] Map = new GameObject[9];

    // �n�}�̉摜���i�[
    public Sprite[] Sprites = new Sprite[9];

    int Mapcount = 0;

    // Start��MonoBehaviour���쐬���ꂽ��A�ŏ���Update�̑O�Ɉ�x�����Ăяo�����
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
    }

    // Update�̓t���[�����ƂɈ�x�Ăяo�����
    void Update()
    {
        // �}�b�v�̎擾������
        Mapcount = gameManager.mapPiece;

        // ���ׂẴ}�b�v�s�[�X���\���ɂ��A�K�؂ȃX�v���C�g��ݒ�
        for (int i = 0; i < Map.Length; i++)
        {
            if (Map[i] != null)
            {
                Map[i].SetActive(i <= Mapcount);
                SpriteRenderer sr = Map[i].GetComponent<SpriteRenderer>();
                if (sr != null && i < Sprites.Length && Sprites[i] != null)
                {
                    sr.sprite = Sprites[i]; // �摜��ύX
                }
            }
        }

        // �}�b�v�̃s�[�X��3����ɓ�������G�l�~�[���f����ύX
        if (Mapcount == 3)
        {
            gameManager.ChangeEnemyModel();
        }
    }
}

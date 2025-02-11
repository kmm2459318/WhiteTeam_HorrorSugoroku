using UnityEngine;

public class MiniMap : MonoBehaviour
{
    public GameManager gameManager;

    // �~�j�}�b�v�̃I�u�W�F�N�g
    public GameObject[] Map = new GameObject[9];

    // �n�}�̉摜���i�[
    public Sprite[] Sprites = new Sprite[9];

    public GameObject currentEnemyModel; // ���݂̃G�l�~�[���f��
    public GameObject newEnemyModel; // �V�����G�l�~�[���f��

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

        // �f�o�b�O���O��ǉ����āAcurrentEnemyModel��newEnemyModel���������ݒ肳��Ă��邩���m�F
        if (currentEnemyModel == null)
        {
            Debug.LogError("currentEnemyModel is not assigned.");
        }
        if (newEnemyModel == null)
        {
            Debug.LogError("newEnemyModel is not assigned.");
        }
    }

    // Update�̓t���[�����ƂɈ�x�Ăяo�����
    void Update()
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
        if (Mapcount == 3)
        {
            ChangeEnemyModel();
        }
    }

    private void ChangeEnemyModel()
    {
        if (currentEnemyModel != null && newEnemyModel != null)
        {
            // �V�����G�l�~�[���f�������̃G�l�~�[���f���̈ʒu�Ɉړ�
            newEnemyModel.transform.position = currentEnemyModel.transform.position;
            newEnemyModel.transform.rotation = currentEnemyModel.transform.rotation;

            // ���̃G�l�~�[���f�����A�N�e�B�u�ɂ���
            currentEnemyModel.SetActive(false);

            // �V�����G�l�~�[���f�����A�N�e�B�u�ɂ���
            newEnemyModel.SetActive(true);

            // �V�����G�l�~�[���f���ɃG�l�~�[�̎d�l��K�p
            EnemySaikoro enemySaikoro = newEnemyModel.GetComponent<EnemySaikoro>();
            if (gameManager.EnemyCopyOn)
            {
                EnemySaikoro enemyCopySaikoro = newEnemyModel.GetComponent<EnemySaikoro>();
            }

            // currentEnemyModel��V�����G�l�~�[���f���ɍX�V
            currentEnemyModel = newEnemyModel;
        }
        else
        {
            Debug.LogError("currentEnemyModel or newEnemyModel is not assigned.");
        }
    }
}
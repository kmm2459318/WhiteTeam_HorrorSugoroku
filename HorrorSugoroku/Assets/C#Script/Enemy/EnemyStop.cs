using UnityEngine;

public class EnemyStop : MonoBehaviour
{
    public GameManager gameManager;
    public string targetTag = "masu";  // �Ώۂ̃^�O
    public float threshold = 0.1f; // �ǂꂭ�炢�̌덷�����e���邩
    public bool rideMasu = false;

    void Start()
    {
        
    }

    void Update()
    {
        CheckPositionMatch();

        if (!gameManager.isPlayerTurn)
        {

        }
    }

    private void CheckPositionMatch()
    {
        GameObject[] masuObjects = GameObject.FindGameObjectsWithTag(targetTag);

        foreach (GameObject masu in masuObjects)
        {
            Vector3 targetPos = masu.transform.position;
            Vector3 currentPos = transform.position;

            // X��Z���W���قڈ�v���Ă��邩����
            if (Mathf.Abs(currentPos.x - targetPos.x) < threshold && Mathf.Abs(currentPos.z - targetPos.z) < threshold && Mathf.Abs(currentPos.y - targetPos.y) < 1f)
            {
                OnMatched(masu);
                break; // ��v����I�u�W�F�N�g�����������烋�[�v�𔲂���
            }
            else
            {
                rideMasu = false;
            }
        }
    }

    private void OnMatched(GameObject masu)
    {
        Debug.Log($"��v: {masu.name} �� {gameObject.name}");
        rideMasu = true;
        // �����ɏ�����ǉ��i��F�A�j���[�V�����Đ��A�X�R�A���Z�Ȃǁj
    }
}

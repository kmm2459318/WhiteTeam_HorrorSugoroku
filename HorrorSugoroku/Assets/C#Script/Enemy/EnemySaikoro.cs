using UnityEngine;

public class EnemySaikoro : MonoBehaviour
{
    void Start()
    {
        // �V�[�����ׂ��ŃI�u�W�F�N�g��ێ����邽�߁ADestroy���Ȃ�
        DontDestroyOnLoad(gameObject);
    }

    public void RollEnemyDice(int min, int max)
    {
        // Enemy�̃T�C�R����U��͈͂��w�肵�ĐU��
        int enemyRoll = Random.Range(min, max + 1);
        Debug.Log("Enemy rolled: " + enemyRoll);

        // �T�C�R���̒l�ɉ���������
        if (enemyRoll == 6)
        {
            Debug.Log("Enemy is lucky! They rolled a 6!");
        }
        else if (enemyRoll <= 3)
        {
            Debug.Log("Enemy rolled a low number: " + enemyRoll + ". They are cautious.");
        }
        else
        {
            Debug.Log("Enemy rolled: " + enemyRoll + ". They proceed with caution.");
        }
    }
}
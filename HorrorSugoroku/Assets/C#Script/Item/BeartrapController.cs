using UnityEngine;

public class BeartrapController : MonoBehaviour
{
    private static int bearTrapCount = 3; // �g���o�T�~�̏����� (�f�o�b�O�p��3����)
    public GameObject player; // �v���C���[�I�u�W�F�N�g
    public GameObject beartrapPrefab; // �g���o�T�~��Prefab
    private bool isTrapActive = false;  // �g���o�T�~���L�����ǂ����̃t���O

    public void PlaceBeartrap()
    {
        if (bearTrapCount > 0)
        {
            bearTrapCount--;
            // �v���C���[�̍��W�Ƀg���o�T�~��z�u
            Instantiate(beartrapPrefab, player.transform.position, Quaternion.identity);
            isTrapActive = true;  // �g���o�T�~���L���ɂȂ���
            Debug.Log("�g���΂��ݔz�u");
        }
    }

    // �g���o�T�~���L�����ǂ�����Ԃ����\�b�h
    public bool IsTrapActive()
    {
        return isTrapActive;
    }

    // �g���o�T�~�̓����蔻�菈��
    public class BeartrapTrigger : MonoBehaviour
    {
        private GameObject enemy;
        private BeartrapController beartrapController; // BeartrapController�ւ̎Q��

        public void SetEnemy(GameObject enemyObj, BeartrapController controller)
        {
            enemy = enemyObj;
            beartrapController = controller;
        }
    }
}

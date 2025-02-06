using UnityEngine;

public class BeartrapController : MonoBehaviour
{
    public GameObject beartrapPrefab; // �g���΂��݂�Prefab
    public Transform spawnPoint; // �g���΂��݂𐶐�����ꏊ
    public EnemySaikoro enemySaikoro; // EnemySaikoro�ւ̎Q��

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) // �^�O��Enemy�̃I�u�W�F�N�g�Ƃ̐ڐG���`�F�b�N
        {
            // ���������G�ɑ΂��ď������s��
            var enemy = other.GetComponent<EnemySaikoro>();
            if (enemy != null)
            {
                enemy.isTrapped = true; // �g���o�T�~�ɂ��������Ƃ��̏���
                Debug.Log("�G���g���o�T�~�ɂ��������I");
            }
        }
    }

    // �{�^���������ƃg���΂��݂𐶐����郁�\�b�h
    public void PlaceBeartrap()
    {
        // �g���΂��݂�Prefab�𐶐�
        Instantiate(beartrapPrefab, spawnPoint.position, Quaternion.identity);
    }
}

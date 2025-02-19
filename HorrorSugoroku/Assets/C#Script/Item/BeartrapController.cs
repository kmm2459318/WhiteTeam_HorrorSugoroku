using UnityEngine;

public class BeartrapController : MonoBehaviour
{
    public GameObject beartrapPrefab; // �g���΂��݂�Prefab
    public Transform spawnPoint; // �g���΂��݂𐶐�����ꏊ
    public EnemySaikoro enemySaikoro; // EnemySaikoro�ւ̎Q��
    public CurseSlider curseSlider; // �􂢃Q�[�W�̊Ǘ�

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) // �^�O��Enemy�̃I�u�W�F�N�g�Ƃ̐ڐG���`�F�b�N
        {
            var enemy = other.GetComponent<EnemySaikoro>();
            if (enemy != null)
            {
                enemy.isTrapped = true; // �g���o�T�~�ɂ��������Ƃ��̏���
                Debug.Log("�G���g���o�T�~�ɂ��������I");

                // �􂢃Q�[�W��10����
                if (curseSlider != null)
                {
                    curseSlider.IncreaseDashPoint(10);
                }
            }
        }
    }

    public void PlaceBeartrap()
    {
        Instantiate(beartrapPrefab, spawnPoint.position, Quaternion.identity);
    }
}

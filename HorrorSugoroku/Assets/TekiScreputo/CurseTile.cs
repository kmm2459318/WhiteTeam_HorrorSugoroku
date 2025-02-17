using UnityEngine;

public class CurseTile : MonoBehaviour
{
    public int gridCellIncreaseAmount = 10; // GridCell ���̎􂢃Q�[�W������
    private CurseSlider curseSlider;

    private void Start()
    {
        // CurseSlider ���V�[��������T��
        curseSlider = FindObjectOfType<CurseSlider>();

        if (curseSlider == null)
        {
            Debug.LogError("CurseSlider ��������܂���I");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("�G�ꂽ��");
        if (other.CompareTag("Player")) // �v���C���[���G�ꂽ�Ƃ�

        {
            Debug.Log("�G�ꂽ��");
            IncreaseCurse(10); // �􂢃Q�[�W��10���₷
        }
    }

    public void IncreaseCurse(int amount)
    {
        if (curseSlider == null)
        {
            curseSlider = FindObjectOfType<CurseSlider>();

            if (curseSlider == null)
            {
                Debug.LogError("CurseSlider ��������܂���I�V�[���ɔz�u����Ă��܂����H");
            }
        }
    }
}

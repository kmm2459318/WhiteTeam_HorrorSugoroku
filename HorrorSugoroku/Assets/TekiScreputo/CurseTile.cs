using UnityEngine;

public class CurseTile : MonoBehaviour
{
    public int gridCellIncreaseAmount = 20; // GridCell ���̎􂢃Q�[�W������
    public CurseSlider curseSlider;
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // CurseSlider ���V�[��������擾
        curseSlider = FindObjectOfType<CurseSlider>();

        if (curseSlider == null)
        {
            Debug.LogError("CurseSlider ���V�[���Ɍ�����܂���I");
        }
    }
    public void CurEs(int increaseAmount)
    {
        Debug.Log($"�􂢃Q�[�W�� {increaseAmount} �����܂����I");

        // curseSlider ��ʂ��� IncreaseDashPoint() ���Ă�
        if (curseSlider != null)
        {
            curseSlider.IncreaseDashPoint(increaseAmount);
        }
        else
        {
            Debug.LogError("curseSlider ���ݒ肳��Ă��܂���I");
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

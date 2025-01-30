using UnityEngine;
using UnityEngine.UI;

public class CurseGauge : MonoBehaviour
{
    public Slider curseSlider;  // �X���C�_�[�i�􂢃Q�[�W�j
    public float maxCurse = 300f;  // �􂢃Q�[�W�̍ő�l
    public float curseIncrement = 5f;  // ��^�[�����Ƃ̑����l
    private float currentCurse = 0f;  // ���݂̎􂢃Q�[�W
    private float turnsPassed = 0f;  // �o�߃^�[����

    void Update()
    {
        // �Q�[���̃^�[�����o�߂��邲�ƂɎ􂢃Q�[�W�𑝉������鏈��
        turnsPassed += Time.deltaTime; // �^�[���������Ԍo�߂ŃJ�E���g�i1�b���Ɓj

        // 1�b���o�߂�����^�[���Ƃ��ăJ�E���g���A�Q�[�W�𑝉�
        if (turnsPassed >= 1f)  // 1�^�[���i1�b�j
        {
            turnsPassed = 0f;  // �^�[���̃J�E���g���Z�b�g
            IncreaseCurse();  // �􂢃Q�[�W�𑝉�
        }

        // �X���C�_�[�̒l���X�V
        curseSlider.value = currentCurse;
    }

    void IncreaseCurse()
    {
        // �􂢃Q�[�W�𑝉��i�ő�l�𒴂��Ȃ��悤�ɐ����j
        currentCurse += curseIncrement;
        if (currentCurse > maxCurse)
        {
            currentCurse = maxCurse;
            // �ő�l�ɒB�����Ƃ��̏����i�v���C���[�Ɉ��e����^����Ȃǁj
        }
    }
}

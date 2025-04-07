using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  // �V�[���Ǘ��p
using System.Collections;

public class JumpScareAnimation : MonoBehaviour
{
    public Animator animator; // �A�j���[�^�[��ݒ�
    public Button startButton; // �A�j���[�V�����J�n�{�^��
    private string triggerName = "StartAttack"; // �g���K�[�̖��O
    private float moveDuration = 0.25f; // �ړ��ɂ����鎞�ԁi�f�t�H���g�l�j
    private Vector3 targetPosition = new Vector3(0f, 0f, 0f); // �ڕW�̈ʒu
    private Vector3 initialPosition; // �ŏ��̈ʒu
    private float timeReset = 2f;
    public JumpScareAnimation jumpScareAnimation; // JumpScareAnimation �N���X�̃C���X�^���X

    void Start()
    {
        // �ŏ��̈ʒu���L�^
        initialPosition = transform.position;

        // �V�[���J�ڌ�ɃA�j���[�V�������J�n���邽�߂̃��X�i�[��ǉ�
        SceneManager.sceneLoaded += OnSceneLoaded;
        // ��: �Q�[���I�[�o�[���ɃA�j���[�V�������J�n
        if (jumpScareAnimation != null)
        {
            jumpScareAnimation.StartAnimation(); // StartAnimation ���\�b�h���Ăяo��
        }
    }

    // �V�[�������[�h���ꂽ��ɃA�j���[�V�������J�n
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // �����W�����v�X�P�A�V�[���ɑJ�ڂ�����A�A�j���[�V�������J�n
        if (scene.name == "Jump Scare") // �V�[�������m�F
        {
            StartCoroutine(DelayStartAnimation()); // �A�j���[�V������x�����ĊJ�n
        }
    }

    // �V�[���J�ڌ�ɏ����x��ăA�j���[�V�������J�n����R���[�`��
    private IEnumerator DelayStartAnimation()
    {
        // �����ҋ@���Ă���A�j���[�V�������J�n
        yield return new WaitForSeconds(0.5f); // �����x�点�Ă���A�j���[�V�������J�n

        // ���W��ڕW�n�_�ɃX���[�Y�Ɉړ�������
        StartCoroutine(MoveObject());
    }

    // �I�u�W�F�N�g���X���[�Y�Ɉړ�������R���[�`��
    private IEnumerator MoveObject()
    {
        float timeElapsed = 0f;

        // ���݂̈ʒu����ڕW�ʒu�Ɍ������Ċ��炩�Ɉړ�
        while (timeElapsed < moveDuration)
        {
            transform.position = Vector3.Lerp(initialPosition, targetPosition, timeElapsed / moveDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // �ŏI�I�Ȉʒu���m���ɐݒ�
        transform.position = targetPosition;

        // �ړ�������������ɃA�j���[�V�������J�n
        if (animator != null)
        {
            animator.enabled = true; // �A�j���[�^�[��L���ɂ���
            animator.SetTrigger(triggerName); // �g���K�[�𔭉΂��ăA�j���[�V�������Đ�
        }

        // �A�j���[�V�����J�n��2�b���ResetObject()���Ăяo��
        yield return new WaitForSeconds(timeReset);
        ResetObject();
    }

    // ���͂��ꂽ�ړ����Ԃ��X�V���郁�\�b�h
    private void UpdateMoveDuration(string input)
    {
        float parsedValue;
        if (float.TryParse(input, out parsedValue))
        {
            moveDuration = parsedValue; // ���͂��ꂽ�l�ňړ����Ԃ��X�V
        }
    }

    private void ResetObject()
    {
        transform.position = initialPosition;
    }

    public void StartAnimation()
    {
        // ���W��ڕW�n�_�ɃX���[�Y�Ɉړ�������
        StartCoroutine(MoveObject());
    }
}

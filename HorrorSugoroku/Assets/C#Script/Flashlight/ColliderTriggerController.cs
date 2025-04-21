using UnityEngine;

public class ColliderTriggerController : MonoBehaviour
{
    [SerializeField] private GameObject playerObject; // �v���C���[�I�u�W�F�N�g�𒼐ڎw��
    [SerializeField] private GlobalLightController globalLightController; // GlobalLightController�ւ̎Q��
    private bool isPlayerInZone = false; // �v���C���[���͈͓��ɂ��邩�ǂ���

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter���Ăяo����܂���"); // �m�F���O
        Debug.Log($"�ΏۃI�u�W�F�N�g: {other.name} (Tag: {other.tag})"); // �Փ˃I�u�W�F�N�g���

        if (other.gameObject == playerObject)
        {
            isPlayerInZone = true;
            Debug.Log($"�w�肳�ꂽ�v���C���[���͈͂ɓ���܂���: {other.name}");
        }
        else
        {
            Debug.Log($"�w�肳�ꂽ�v���C���[�ł͂���܂���: {other.name}");
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("OnTriggerExit���Ăяo����܂���"); // �m�F���O
        Debug.Log($"�ΏۃI�u�W�F�N�g: {other.name} (Tag: {other.tag})"); // ���E�I�u�W�F�N�g���

        if (other.gameObject == playerObject)
        {
            isPlayerInZone = false;
            Debug.Log($"�w�肳�ꂽ�v���C���[���͈͂𗣂�܂���: {other.name}");
        }
        else
        {
            Debug.Log($"�w�肳�ꂽ�v���C���[�ł͂���܂���: {other.name}");
        }
    }

    void Update()
    {
        Debug.Log($"Update���`�F�b�N: isPlayerInZone = {isPlayerInZone}"); // �t���O��Ԃ���Ɋm�F

        // A�L�[��������邩�ǂ������m�F
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("A�L�[��������܂���");
        }

        // �v���C���[���͈͓��ɂ���ꍇ�̂�A�L�[����t
        if (isPlayerInZone && Input.GetKeyDown(KeyCode.A))
        {
            if (globalLightController != null)
            {
                globalLightController.ApplyLightState();
                Debug.Log("GlobalLightController �� ApplyLightState ���Ăяo���܂���");
            }
            else
            {
                Debug.LogWarning("GlobalLightController ���ݒ肳��Ă��܂���I");
            }
        }
    }
}
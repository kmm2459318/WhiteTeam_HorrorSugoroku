using UnityEngine;

public class ColliderTriggerController : MonoBehaviour
{
    [SerializeField] private GameObject playerObject; // �v���C���[�I�u�W�F�N�g
    [SerializeField] private GameObject triggerObject; // �g���K�[�I�u�W�F�N�g
    [SerializeField] private GlobalLightController globalLightController; // GlobalLightController�̎Q��

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == playerObject)
        {
            Debug.Log($"�v���C���[ '{playerObject.name}' �� '{triggerObject.name}' �̃G���A�ɓ���܂����I");
            if (globalLightController != null)
            {
                globalLightController.SetPlayerInZone(true); // �v���C���[���G���A���ɂ���ƒʒm
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == playerObject)
        {
            Debug.Log($"�v���C���[ '{playerObject.name}' �� '{triggerObject.name}' �̃G���A���o�܂����I");
            if (globalLightController != null)
            {
                globalLightController.SetPlayerInZone(false); // �v���C���[���G���A�O�ƒʒm
            }
        }
    }
}
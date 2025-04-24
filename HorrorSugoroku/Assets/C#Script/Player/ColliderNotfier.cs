using UnityEngine;

public class ColliderNotifier : MonoBehaviour
{
    [SerializeField] private PlayerSoundController playerSoundController; // PlayerSoundController�̎Q��
    [SerializeField] private string colliderName; // ���̃R���C�_�[�̖��O

    private void OnTriggerEnter(Collider other)
    {
        if (playerSoundController != null)
        {
            playerSoundController.OnColliderEnter(colliderName, other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (playerSoundController != null)
        {
            playerSoundController.OnColliderExit(colliderName, other);
        }
    }
}
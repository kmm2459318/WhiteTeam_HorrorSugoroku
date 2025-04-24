using UnityEngine;

public class ColliderNotifier : MonoBehaviour
{
    [SerializeField] private PlayerSoundController playerSoundController; // PlayerSoundControllerの参照
    [SerializeField] private string colliderName; // このコライダーの名前

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
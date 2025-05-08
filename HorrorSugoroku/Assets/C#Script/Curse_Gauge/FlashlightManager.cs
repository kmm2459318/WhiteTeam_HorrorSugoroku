using UnityEngine;

public class FlashlightManager : MonoBehaviour
{
    [SerializeField] public GameObject flashlightModel;
    [SerializeField] private GameObject flashlightPrefab;

    [SerializeField] private AudioSource audioSource; // AudioSource をインスペクターで設定
    [SerializeField] private AudioClip turnOffSound;  // 懐中電灯が消えるときの効果音

    public void DeactivateFlashlight()
    {
        if (flashlightModel != null)
        {
            // 効果音を先に鳴らす
            if (audioSource != null && turnOffSound != null)
            {
                audioSource.PlayOneShot(turnOffSound);
            }
            else
            {
                Debug.LogWarning("AudioSourceまたはAudioClipが設定されていません");
            }

            flashlightModel.SetActive(false);
            Debug.Log("懐中電灯を非アクティブにしました");
        }
        else
        {
            Debug.LogError("flashlightModelが設定されていません");
        }
    }


    public void PlaceFlashlightUnderPlayer(Transform playerTransform)
    {
        if (flashlightPrefab != null)
        {
            Vector3 position = playerTransform.position;
            position.y -= 1.5f;
            Quaternion rotation = Quaternion.Euler(0, 30, 0);
            Debug.Log("懐中電灯を配置します: " + position);
            Instantiate(flashlightPrefab, position, rotation);
        }
        else
        {
            Debug.LogError("flashlightPrefabが設定されていません");
        }
    }
}

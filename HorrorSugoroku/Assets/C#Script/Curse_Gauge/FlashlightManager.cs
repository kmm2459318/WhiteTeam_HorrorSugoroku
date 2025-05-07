using UnityEngine;

public class FlashlightManager : MonoBehaviour
{
    [SerializeField] public GameObject flashlightModel;     // 懐中電灯のモデル
    [SerializeField] private GameObject flashlightPrefab;   // 懐中電灯のプレハブ

    [SerializeField] private AudioSource audioSource;       // 音を鳴らすAudioSource
    [SerializeField] private AudioClip switchOffClip;       // 懐中電灯が消えるときの音

    public void DeactivateFlashlight()
    {
        if (flashlightModel != null)
        {
            flashlightModel.SetActive(false);
            Debug.Log("懐中電灯を非アクティブにしました");

            // 効果音を鳴らす
            if (audioSource != null && switchOffClip != null)
            {
                audioSource.PlayOneShot(switchOffClip);
            }
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

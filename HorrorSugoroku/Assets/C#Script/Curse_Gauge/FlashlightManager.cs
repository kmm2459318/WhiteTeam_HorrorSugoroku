using UnityEngine;

public class FlashlightManager : MonoBehaviour
{
    [SerializeField] private GameObject flashlightModel; // 懐中電灯のモデル
    [SerializeField] private GameObject flashlightPrefab; // 懐中電灯のプレハブ

    public void DeactivateFlashlight()
    {
        if (flashlightModel != null)
        {
            flashlightModel.SetActive(false);
            
        }
    }

    public void PlaceFlashlightUnderPlayer(Transform playerTransform)
    {
        if (flashlightPrefab != null)
        {
            Vector3 position = playerTransform.position;
            position.y -= 1.5f; // プレイヤーの真下より少し低く配置（調整）

            // 回転を指定する（例: 30度傾ける）
            Quaternion rotation = Quaternion.Euler(0, 30, 0);

            Instantiate(flashlightPrefab, position, rotation);
        }
    }
}
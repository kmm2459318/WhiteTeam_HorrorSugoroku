using UnityEngine;

public class FlashlightManager : MonoBehaviour
{
    [SerializeField] public GameObject flashlightModel; // 懐中電灯のモデルをpublicに変更
    [SerializeField] private GameObject flashlightPrefab; // 懐中電灯のプレハブ

    public void DeactivateFlashlight()
    {
        if (flashlightModel != null)
        {
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
            position.y -= 1.5f; // プレイヤーの真下より少し低く配置（調整）

            // 回転を指定する（例: 30度傾ける）
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
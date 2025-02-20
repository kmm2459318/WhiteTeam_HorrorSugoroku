using UnityEngine;

public class FlashlightManager : MonoBehaviour
{
    [SerializeField] private GameObject flashlightModel; // �����d���̃��f��
    [SerializeField] private GameObject flashlightPrefab; // �����d���̃v���n�u

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
            position.y -= 1.5f; // �v���C���[�̐^����菭���Ⴍ�z�u�i�����j

            // ��]���w�肷��i��: 30�x�X����j
            Quaternion rotation = Quaternion.Euler(0, 30, 0);

            Instantiate(flashlightPrefab, position, rotation);
        }
    }
}
using UnityEngine;

public class FlashlightManager : MonoBehaviour
{
    [SerializeField] public GameObject flashlightModel; // �����d���̃��f����public�ɕύX
    [SerializeField] private GameObject flashlightPrefab; // �����d���̃v���n�u

    public void DeactivateFlashlight()
    {
        if (flashlightModel != null)
        {
            flashlightModel.SetActive(false);
            Debug.Log("�����d�����A�N�e�B�u�ɂ��܂���");
        }
        else
        {
            Debug.LogError("flashlightModel���ݒ肳��Ă��܂���");
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

            Debug.Log("�����d����z�u���܂�: " + position);
            Instantiate(flashlightPrefab, position, rotation);
        }
        else
        {
            Debug.LogError("flashlightPrefab���ݒ肳��Ă��܂���");
        }
    }
}
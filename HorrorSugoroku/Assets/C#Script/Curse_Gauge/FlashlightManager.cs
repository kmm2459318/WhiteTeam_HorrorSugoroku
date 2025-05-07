using UnityEngine;

public class FlashlightManager : MonoBehaviour
{
    [SerializeField] public GameObject flashlightModel;     // �����d���̃��f��
    [SerializeField] private GameObject flashlightPrefab;   // �����d���̃v���n�u

    [SerializeField] private AudioSource audioSource;       // ����炷AudioSource
    [SerializeField] private AudioClip switchOffClip;       // �����d����������Ƃ��̉�

    public void DeactivateFlashlight()
    {
        if (flashlightModel != null)
        {
            flashlightModel.SetActive(false);
            Debug.Log("�����d�����A�N�e�B�u�ɂ��܂���");

            // ���ʉ���炷
            if (audioSource != null && switchOffClip != null)
            {
                audioSource.PlayOneShot(switchOffClip);
            }
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
            position.y -= 1.5f;

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

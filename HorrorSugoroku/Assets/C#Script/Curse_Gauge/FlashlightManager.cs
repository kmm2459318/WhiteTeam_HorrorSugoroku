using UnityEngine;

public class FlashlightManager : MonoBehaviour
{
    [SerializeField] public GameObject flashlightModel;
    [SerializeField] private GameObject flashlightPrefab;

    [SerializeField] private AudioSource audioSource; // AudioSource ���C���X�y�N�^�[�Őݒ�
    [SerializeField] private AudioClip turnOffSound;  // �����d����������Ƃ��̌��ʉ�

    public void DeactivateFlashlight()
    {
        if (flashlightModel != null)
        {
            // ���ʉ����ɖ炷
            if (audioSource != null && turnOffSound != null)
            {
                audioSource.PlayOneShot(turnOffSound);
            }
            else
            {
                Debug.LogWarning("AudioSource�܂���AudioClip���ݒ肳��Ă��܂���");
            }

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

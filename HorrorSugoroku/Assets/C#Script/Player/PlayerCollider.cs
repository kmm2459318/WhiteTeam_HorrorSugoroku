using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // �}�X�ɐG�ꂽ�Ƃ�
        if (other.CompareTag("masu"))
        {
            // �}�X�̃����_���[��\��
            Renderer renderer = other.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.enabled = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // �}�X���痣�ꂽ�Ƃ�
        if (other.CompareTag("masu"))
        {
            // �}�X�̃����_���[���\��
            Renderer renderer = other.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.enabled = false;
            }
        }
    }
}
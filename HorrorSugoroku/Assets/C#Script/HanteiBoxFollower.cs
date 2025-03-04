using UnityEngine;

public class HanteiBoxFollower : MonoBehaviour
{
    public GameObject player; // Player�I�u�W�F�N�g
    public GameObject hanteiBox; // HanteiBox�I�u�W�F�N�g
    public float heightOffset = -1.0f; // �����̃I�t�Z�b�g
    public float forwardOffset = 1.0f; // �O���̃I�t�Z�b�g

    void Update()
    {
        if (player != null && hanteiBox != null)
        {
            // Player�̈ʒu��HanteiBox��Ǐ]������
            Vector3 newPosition = player.transform.position;
            newPosition.y += heightOffset; // �����𒲐�
            newPosition += player.transform.forward * forwardOffset; // �O���ɃI�t�Z�b�g
            hanteiBox.transform.position = newPosition;
        }
    }
}
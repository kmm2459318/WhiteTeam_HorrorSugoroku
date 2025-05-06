using UnityEngine;

public class PlaceInFrontOfplayer : MonoBehaviour
{
    public Transform player;           // �v���C���[��Transform
    public float distanceInFront = 2f; // �v���C���[�̑O�ɏo������
    public bool matchRotation = true;  // �v���C���[�̌����ɍ��킹�邩�ǂ���

    void Update()
    {
        if (player != null)
        {
            // �v���C���[�̑O���ʒu���v�Z
            Vector3 frontPosition = player.position + player.forward * distanceInFront;

            // �����̓v���C���[�ɍ��킹��i�K�v�Ȃ�j
           // frontPosition.y = player.position.y;

            // �I�u�W�F�N�g�̈ʒu���X�V
            transform.position = frontPosition;

            // �������v���C���[�ɍ��킹��i�K�v�ȏꍇ�j
            if (matchRotation)
            {
                transform.rotation = player.rotation;
            }
        }
    }
}

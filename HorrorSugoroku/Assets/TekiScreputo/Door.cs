using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator doorAnimator; // �h�A�̃A�j���[�^�[
    public string openAnimation = "Open"; // �h�A���J����A�j���[�V�����̖��O
    public string closeAnimation = "Close"; // �h�A��߂�A�j���[�V�����̖��O
    public bool isOpen = false; // �h�A���J���Ă��邩�ǂ���
    public float interactionRange = 3f; // �v���C���[���h�A���J���邽�߂ɕK�v�ȋ���

    private Transform player; // �v���C���[�� Transform
    private PlayerInventory playerInventory; // �v���C���[�̃C���x���g���Q��
    public string requiredItem = "��"; // �K�v�ȃA�C�e��
    void Start()
    {
        // �v���C���[���V�[�����̃^�O "Player" �����I�u�W�F�N�g�ɐݒ�
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // �v���C���[�̃C���x���g���X�N���v�g���擾
        playerInventory = player.GetComponent<PlayerInventory>();
    }

    void Update()
    {
        // �v���C���[���h�A�̋߂��ɂ��邩�m�F
        float distance = Vector3.Distance(player.position, transform.position);

        if (distance <= interactionRange) // �C���^���N�V�����͈͓��ɂ���ꍇ
        {
            if (distance <= interactionRange && Input.GetKeyDown(KeyCode.G)) // �uE�v�L�[�Ńh�A���J����/�߂�
            {
                if (isOpen)
                {
                    CloseDoor(); // �h�A��߂�
                }
                else
                {
                    // ���������Ă��邩�ǂ����`�F�b�N
                    if (playerInventory != null && playerInventory.HasItem("��"))
                    {
                      
                        OpenDoor(); // ��������΃h�A���J����
                        Debug.Log("�����g���Ĕ����J����");
                        playerInventory.RemoveItem("��"); // �����g��
                    }
                    else
                    {
                        Debug.Log("��������܂���B"); // �����Ȃ���ΊJ�����Ȃ�
                    }
                }
            }
        }

        // �h�A���J���郁�\�b�h
        void OpenDoor()
        {
            if (doorAnimator != null)
            {
                doorAnimator.Play(openAnimation); // �A�j���[�V�������Đ�
                isOpen = true;
            }
        }

        // �h�A��߂郁�\�b�h
        void CloseDoor()
        {
            if (doorAnimator != null)
            {
                doorAnimator.Play(closeAnimation); // �A�j���[�V�������Đ�
                isOpen = false;
            }
        }
    }
}
using System.Collections;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    public Animator doorAnimator; // �h�A�̃A�j���[�^�[
    public float interactionRange = 3f;
    private bool isOpen = false; // �h�A�̏��


    private Transform player; // �v���C���[�� Transform
    public PlayerInventory playerInventory; // �v���C���[�̃C���x���g���Q��
    public string requiredItem ; // �K�v�ȃA�C�e��

  
    public GameObject doorPanel; // UI�̃p�l��
    public TextMeshProUGUI doorText; // UI�̃e�L�X�g
    public Button okButton;   // OK�{�^��
    public Button cancelButton; // �L�����Z���{�^��
    void Start()
    {
        // �v���C���[���V�[�����̃^�O "Player" �����I�u�W�F�N�g�ɐݒ�
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // �v���C���[�̃C���x���g���X�N���v�g���擾
        playerInventory = player.GetComponent<PlayerInventory>();


        if (doorPanel != null)
        {
            doorPanel.SetActive(false); // �ŏ���UI���\��
        }
        // �{�^���̃N���b�N�C�x���g��o�^
        //if (okButton != null)
        //    okButton.onClick.AddListener(OpenDoorConfirmed);

        //if (cancelButton != null)
        //    cancelButton.onClick.AddListener(CloseUI);
    }

    void Update()
    {
        // �v���C���[���h�A�̋߂��ɂ��邩�m�F
        float distance = Vector3.Distance(player.position, transform.position);

        if (Input.GetKeyDown(KeyCode.G)) // �uE�v�L�[�Ńh�A���J����/�߂�
        {
            if (doorPanel.activeSelf)
            {
              //  Debug.Log("aaa");
                //CloseDoor(); // �h�A��߂�
                CloseUI(); // UI���J���Ă��������
            }
        }

        if (distance <= interactionRange) // �C���^���N�V�����͈͓��ɂ���ꍇ
        {
            if (distance <= interactionRange && Input.GetKeyDown(KeyCode.G)) // �uE�v�L�[�Ńh�A���J����/�߂�
            {
                //if (doorUI.activeSelf)
                //{
                //    Debug.Log("aaa");
                //    //CloseDoor(); // �h�A��߂�
                //    CloseUI(); // UI���J���Ă��������
                //}
                 if (!isOpen)
                {
                    // ���������Ă��邩�ǂ����`�F�b�N
                    if (playerInventory != null && playerInventory.HasItem(requiredItem))
                    {

                        //  ShowDoorUI();
                        OpenDoorConfirmed(); // �����g���ăh�A���J��
                    }
                    else
                    {
                        Debug.Log("��������܂���B"); // �����Ȃ���ΊJ�����Ȃ�
                    }
                }
            }
        }


        //void ShowDoorUI()
        //{
        //    if (doorUI != null)
        //    {
        //        doorUI.SetActive(true);
        //        Time.timeScale = 0;
        //    }
        //}

        IEnumerator ClosUI(string message, float delay)
        {
            yield return new WaitForSeconds(delay);
            //Debug.Log("iii");
            if (doorPanel != null || doorText != null)
            {
               // Debug.Log("uuu");
                doorPanel.SetActive(false);
                doorText.text = message;
                isOpen = false;
                Time.timeScale = 1; // �Q�[�����ĊJ
            }
        }
        void CloseUI()
        {
            bool wasPaused = false;

            if (doorPanel != null && doorPanel.activeSelf)
            {
                doorPanel.SetActive(false);
                wasPaused = true;
            }

            // UI���J���Ă����ꍇ�̂�Time.timeScale��߂�
            if (wasPaused)
            {
                Debug.Log("�Q�[���ĊJ�I");
                Time.timeScale = 1;
            }
        }
        //void OpenDoorConfirmed()
        //{
        //    CloseUI(); // UI�����
        //    OpenDoor(); // �h�A���J��
        //    playerInventory.RemoveItem(requiredItem);
        //}

        void OpenDoorConfirmed()
        {
            OpenDoor(); // �h�A���J��
            if (doorPanel != null)
            {
                doorPanel.SetActive(true);
               // Time.timeScale = 0;
            }
          
            playerInventory.RemoveItem(requiredItem); // �����C���x���g������폜
        }


        // �h�A���J���郁�\�b�h
        void OpenDoor()
        {
            if (doorAnimator != null)
            {
                Debug.Log("dadwed");
                doorAnimator.SetBool("isOpen", true); // �A�j���[�V�������Đ�
                isOpen = true;
                Debug.Log("�h�A���J���܂���"); // ���O���o��

                Destroy(gameObject);
            }
        }

        // �h�A��߂郁�\�b�h
        //void CloseDoor()
        //{
        //    if (doorAnimator != null)
        //    {
        //        doorAnimator.SetBool("isOpen", false); // �A�j���[�V�������Đ�
        //        isOpen = false;
        //    }
        //}
    }
}

using System.Collections;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    public Animator doorAnimator; // �h�A�̃A�j���[�^�[
    public Animator doorAnimatorLeft;
    public float interactionRange = 3f;
    private bool isOpen = false; // �h�A�̏��


    private Transform player; // �v���C���[�� Transform
    public PlayerInventory playerInventory; // �v���C���[�̃C���x���g���Q��
    public string requiredItem; // �K�v�ȃA�C�e��


    public GameObject doorPanel; // UI�̃p�l��
    public TextMeshProUGUI doorText; // UI�̃e�L�X�g
    public float messageDisplayTime = 2f; // ���b�Z�[�W��\�����鎞�ԁi�b�j
    //public TextMeshProUGUI messgeText;
    //public Button okButton;   // OK�{�^��
    //public Button cancelButton; // �L�����Z���{�^��

    public GameObject hiddenArea; // �\���������}�X�i�h�A���J���ƕ\���j
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerInventory = player.GetComponent<PlayerInventory>();

        if (doorPanel != null)
        {
            doorPanel.SetActive(false); // �ŏ���UI���\��
        }

        if (hiddenArea != null)
        {
            hiddenArea.SetActive(false); // �ŏ��͉B�ꂽ�G���A���\��
        }
    }
    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        if (distance <= interactionRange && Input.GetKeyDown(KeyCode.G)) // G�L�[�Ńh�A���J����
        {
            if (!isOpen)
            {
                if (playerInventory != null && playerInventory.HasItem(requiredItem))
                {
                    OpenDoorConfirmed(); // �����g���ăh�A���J��
                }
                else
                {
                    Debug.Log("��������܂���B"); // �����Ȃ���ΊJ�����Ȃ�
                }
            }
        }
    }
    // UI��\�����Ĉ�莞�Ԍ�ɕ���
    IEnumerator ShowMessageAndCloseUI(string message, float delay)
    {
        if (doorPanel != null)
        {
            doorPanel.SetActive(true);
        }

        if (doorText != null)
        {
            doorText.text = message;
        }

        yield return new WaitForSeconds(delay);

        if (doorPanel != null)
        {
            doorPanel.SetActive(false);
        }
    }
    // �h�A���J���郁�\�b�h
    void OpenDoorConfirmed()
    {
        OpenDoor(); // �h�A���J��
        playerInventory.RemoveItem(requiredItem); // �����C���x���g������폜
    }

    void OpenDoor()
    {
        if (doorAnimator != null)
        {
            doorAnimator.SetBool("isOpen", true);
            doorAnimatorLeft.SetBool("isOpen", true);
        }

        isOpen = true;
        Debug.Log($"{requiredItem} �̃h�A���J���܂���");

        StartCoroutine(ShowMessageAndCloseUI($"{requiredItem} �̃h�A���J���܂���", messageDisplayTime));

        if (hiddenArea != null)
        {
            hiddenArea.SetActive(true);
        }

        Destroy(gameObject); // �h�A�I�u�W�F�N�g���폜
    }
}

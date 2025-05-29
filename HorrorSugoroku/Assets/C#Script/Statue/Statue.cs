using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngineInternal;

public class Statue : MonoBehaviour
{
    public GameManager gameManager;
    public PlayerSaikoro playerSaikoro;

    //�l�`��u���G���A
    public GameObject[] PutDownArea = new GameObject[4];
    //�l�`�p�̔�
    public GameObject[] Doll = new GameObject[4];
    //���C�g
    public Light Clearlight;
    public int PutDoll; //�l�`��u������
    private float AddIntensity = 1000f; //���̋����̑�����
    public float WaitTime = 7f; // �҂����ԁi�b�j

    private float Timer = 0f; // �^�C�}�[

    private GameObject lastHighlightedObject = null;

    private bool hasClicked = false;
    public bool isExitDoor = false; // �E�o�h�A�ł��邱�Ƃ��킩��^�O
    private bool sceneLoaded = false; //��d�ǂݍ��ݖh�~�t���O

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // �l�`���\��
        for (int i = 0; i < Doll.Length; i++)
        {
            if (Doll[i] != null)
            {
                Doll[i].SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !hasClicked)
        {
            hasClicked = true;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                lastHighlightedObject = hit.collider.gameObject;
                // �^�O�� "DollArea"�̏ꍇ
                if (hit.collider.CompareTag("DollArea"))
                {
                    //�v���C���[���ړ��������Ă�����
                    if (!playerSaikoro.idoutyu && !gameManager.isPlayerTurn)
                    {
                        Debug.Log("�ړ��������G��Ă���");
                        if (IsLookingAtObject(hit.collider.gameObject))
                        {
                            //�l�`�������Ă��鐔���m�F
                            if (gameManager.CanPlaceDoll())
                            {
                                for (int i = 0; i < PutDownArea.Length; i++)
                                {
                                    if (PutDownArea[i] == hit.collider.gameObject) // �ǂ̃G���A���N���b�N����������
                                    {
                                        if (Doll[i] != null && !Doll[i].activeSelf)
                                        {
                                            Doll[i].SetActive(true); // �Ή�����l�`�̂ݕ\��
                                            PutDoll++; // �l�`��u���������J�E���g
                                            gameManager.PlaceDoll();

                                            CheckAllDoll(); //�l�`��S���u�������̔���
                                        }
                                    }
                                }
                                lastHighlightedObject.GetComponent<Outline>().enabled = false;
                            }
                            else
                            {
                                Debug.Log("���������ɂȂɂ���u��������");
                            }
                        }
                    }
                }
            }

            StartCoroutine(ResetClick()); // �t���O���Z�b�g�R���[�`���Ăяo��
        }

        //�E�o�\�ɂȂ����������������
        if (isExitDoor && !sceneLoaded)
        {
            Debug.Log("�E�o���o");
            Clearlight.intensity += AddIntensity * Time.deltaTime;
            // �^�C�}�[��i�߂�
            Timer += Time.deltaTime;

            //���̎��Ԃ𒴂�����V�[���ǂݍ���
            if (Timer >= WaitTime)
            {
                sceneLoaded = true; // ��d�ǂݍ��ݖh�~
                GameState.IsGameClear = true;
                SceneManager.LoadScene("Ending");
            }
        }

        
    }

    IEnumerator ResetClick()
    {
        yield return new WaitForSeconds(0.1f); // ���d�N���b�N�h�~����
        hasClicked = false;
    }

    bool IsLookingAtObject(GameObject obj)
    {
        Vector3 directionToObject = (obj.transform.position - Camera.main.transform.position).normalized;
        float dotProduct = Vector3.Dot(Camera.main.transform.forward, directionToObject);

        return dotProduct > 0.8f; // **0.8�ȏ�Ȃ�v���C���[�̎��������ɂ���**
    }

    //�l�`��S���u�������̔���
    void CheckAllDoll()
    {
        for(int i =0; i < Doll.Length; i++)
        {
            if (!Doll[i].active)
            {
                return;
            }
        }

        isExitDoor = true;

    }

}

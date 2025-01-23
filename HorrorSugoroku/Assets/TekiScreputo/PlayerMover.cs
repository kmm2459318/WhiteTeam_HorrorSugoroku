using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class PlayerMover : MonoBehaviour
{
    public  PlayerSaikoro playerSaikoro; // �Ď��Ώۂ̃X�N���v�g
   public GridCell gridCell;
    private bool lastState = false; // �O��̏��

    void Start()
    {
        // �X�N���v�g���A�T�C������Ă��Ȃ��ꍇ�A�����I�Ɏ擾
        if (playerSaikoro == null)
        {
            playerSaikoro = GetComponent<PlayerSaikoro>();
        }
    }

    void Update()
    {
        if (playerSaikoro == null)
        {
            Debug.LogError("TargetScript���ݒ肳��Ă��܂���B");
            return;
        }

        // ��Ԃ̕ω����Ď�
        if (lastState && !playerSaikoro.idoutyu)
        {
            Debug.Log("TargetScript��idoutyu��false�ɂȂ�܂����I");
            PlayerMoverExecuteEvent();
        }
        // ��Ԃ��X�V
        lastState = playerSaikoro.idoutyu;
    }

    private void PlayerMoverExecuteEvent()
    {
        // �K�v�ȏ������L�q

        gridCell.ExecuteEvent();
    }

} // �K�v�ȃC�x���g�����������ɒǉ�
    // ��: �Q�[���̏I���A�v���C���[�̒�~�Ȃ�



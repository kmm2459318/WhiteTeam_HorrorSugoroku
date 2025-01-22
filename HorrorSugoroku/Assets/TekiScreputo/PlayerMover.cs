using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class PlayerMover : MonoBehaviour
{
    public  PlayerSaikoro playerSaikoro; // 監視対象のスクリプト
   public GridCell gridCell;
    private bool lastState = false; // 前回の状態

    void Start()
    {
        // スクリプトがアサインされていない場合、自動的に取得
        if (playerSaikoro == null)
        {
            playerSaikoro = GetComponent<PlayerSaikoro>();
        }
    }

    void Update()
    {
        if (playerSaikoro == null)
        {
            Debug.LogError("TargetScriptが設定されていません。");
            return;
        }

        // 状態の変化を監視
        if (lastState && !playerSaikoro.idoutyu)
        {
            Debug.Log("TargetScriptのidoutyuがfalseになりました！");
            PlayerMoverExecuteEvent();
        }
        // 状態を更新
        lastState = playerSaikoro.idoutyu;
    }

    private void PlayerMoverExecuteEvent()
    {
        // 必要な処理を記述

        gridCell.ExecuteEvent();
    }

} // 必要なイベント処理をここに追加
    // 例: ゲームの終了、プレイヤーの停止など



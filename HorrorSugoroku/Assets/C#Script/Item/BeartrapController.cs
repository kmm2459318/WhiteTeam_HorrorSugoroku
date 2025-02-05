using UnityEngine;

public class BeartrapController : MonoBehaviour
{
    private static int bearTrapCount = 3; // トラバサミの所持数 (デバッグ用に3つ所持)
    public GameObject player; // プレイヤーオブジェクト
    public GameObject beartrapPrefab; // トラバサミのPrefab
    private bool isTrapActive = false;  // トラバサミが有効かどうかのフラグ

    public void PlaceBeartrap()
    {
        if (bearTrapCount > 0)
        {
            bearTrapCount--;
            // プレイヤーの座標にトラバサミを配置
            Instantiate(beartrapPrefab, player.transform.position, Quaternion.identity);
            isTrapActive = true;  // トラバサミが有効になった
            Debug.Log("トラばさみ配置");
        }
    }

    // トラバサミが有効かどうかを返すメソッド
    public bool IsTrapActive()
    {
        return isTrapActive;
    }

    // トラバサミの当たり判定処理
    public class BeartrapTrigger : MonoBehaviour
    {
        private GameObject enemy;
        private BeartrapController beartrapController; // BeartrapControllerへの参照

        public void SetEnemy(GameObject enemyObj, BeartrapController controller)
        {
            enemy = enemyObj;
            beartrapController = controller;
        }
    }
}

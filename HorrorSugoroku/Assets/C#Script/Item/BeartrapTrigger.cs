using UnityEngine;

public class BeartrapTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("敵がトラばさみに引っ掛かった！！");

            EnemySaikoro enemy = other.GetComponent<EnemySaikoro>();
            if (enemy != null)
            {
                enemy.isTrapped = true; // ここで敵の動きを止める
                Debug.Log("敵が動けなくなった！");
            }

            // トラバサミを削除（次のターンで消すなら別の処理を追加）
            Destroy(gameObject);
        }
    }
}

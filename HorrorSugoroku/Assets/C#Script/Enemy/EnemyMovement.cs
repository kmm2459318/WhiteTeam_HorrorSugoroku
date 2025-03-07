using UnityEngine;

public class FixedYPosition : MonoBehaviour
{
    private float fixedY; // 固定するY座標

    void Start()
    {
        fixedY = transform.position.y; // 初期のY座標を固定
    }

    void Update()
    {
        Vector3 position = transform.position;
        position.y = fixedY; // Y座標を固定
        transform.position = position;
    }
}
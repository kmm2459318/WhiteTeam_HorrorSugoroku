using UnityEngine;

public class FixedYPosition : MonoBehaviour
{
    private float fixedY; // �Œ肷��Y���W

    void Start()
    {
        fixedY = transform.position.y; // ������Y���W���Œ�
    }

    void Update()
    {
        Vector3 position = transform.position;
        position.y = fixedY; // Y���W���Œ�
        transform.position = position;
    }
}
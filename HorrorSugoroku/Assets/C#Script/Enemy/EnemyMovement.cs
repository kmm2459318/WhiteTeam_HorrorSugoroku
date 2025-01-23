using UnityEngine;

public class FixedYPosition : MonoBehaviour
{
    private float fixedY; // ŒÅ’è‚·‚éYÀ•W

    void Start()
    {
        fixedY = transform.position.y; // ‰Šú‚ÌYÀ•W‚ğŒÅ’è
    }

    void Update()
    {
        Vector3 position = transform.position;
        position.y = fixedY; // YÀ•W‚ğŒÅ’è
        transform.position = position;
    }
}
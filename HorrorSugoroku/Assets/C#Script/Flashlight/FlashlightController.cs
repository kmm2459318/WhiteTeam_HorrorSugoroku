using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    [Header("�����d���ݒ�")]
    public Light flashlight; // �����d���̃��C�g

    void Start()
    {
        flashlight.enabled = true; // ���C�g�͏��ON
    }
}

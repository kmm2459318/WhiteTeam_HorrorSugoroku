using UnityEngine;

public class BreakerController : MonoBehaviour
{
    public Light elevatorLight;
    public bool breaker = false;

    void Start()
    {
        // GameObject�^�̔z��cubes�ɁA"box"�^�O�̂����I�u�W�F�N�g�����ׂĊi�[
        GameObject[] cubes = GameObject.FindGameObjectsWithTag("light");
    }

    void Update()
    {
        //�u���[�J�[ON(if�̓��e�͂̂��ɕς��܂�)
        if (Input.GetKeyDown(KeyCode.B) && !breaker)
        {
            BreakerOn();
        }
    }

    void BreakerOn()
    {
        breaker = true;
        elevatorLight.enabled = true;
    }
}

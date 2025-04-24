using UnityEngine;

public class BreakerController : MonoBehaviour
{
    public Light elevatorLight;
    public Light elevatorLight1F;
    public Light elevatorLight2F;
    public Light elevatorLightB1F;
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
        elevatorLight1F.enabled = true;
        elevatorLight2F.enabled = true;
        elevatorLightB1F.enabled = true;
    }
}
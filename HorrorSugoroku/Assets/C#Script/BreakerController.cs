using UnityEngine;

public class BreakerController : MonoBehaviour
{
    public GameObject ElevatorMasu;

    void Start()
    {
        // GameObject�^�̔z��cubes�ɁA"box"�^�O�̂����I�u�W�F�N�g�����ׂĊi�[
        GameObject[] cubes = GameObject.FindGameObjectsWithTag("light");
    }

    void Update()
    {
        //�u���[�J�[ON(if�̓��e�͂̂��ɕς��܂�)
        if (Input.GetKeyDown(KeyCode.B))
        {
            BreakerOn();
        }
    }

    void BreakerOn()
    {
        ElevatorMasu.SetActive(true);
    }
}

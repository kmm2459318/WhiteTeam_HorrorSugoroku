using SmoothigTransform;
using UnityEngine;
using UnityEngine.UI;

public class ElevatorTile : MonoBehaviour
{
    public SmoothTransform smoothTransform;
    public ElevatorController controller;

    public GameObject ElevatorMasu;

    [Header("�G���x�[�^�[�̏��i���������j")]
    public Transform elevatorPlatform;

    [Header("�Ώۃv���C���[")]
    public Transform player;

    private bool isPlayerOnThisTile = false;
    private void Start()
    {
        //smoothTransform = player.GetComponent<SmoothTransform>();
    }

    private void Update()
    {
        if (isPlayerOnThisTile && smoothTransform != null)
        {
            Debug.Log(elevatorPlatform.position.y);
            smoothTransform.SetTargetY(elevatorPlatform.position.y);
        }

        if (controller.isMoving)
        {
            smoothTransform.TargetPosition = ElevatorMasu.transform.position + new Vector3(0, 1.15f, 0);
        }
    }

    public void SetPlayerOnTile(bool onTile)
    {
        isPlayerOnThisTile = onTile;
    }
}

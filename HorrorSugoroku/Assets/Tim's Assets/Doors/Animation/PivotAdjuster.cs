using UnityEngine;

public class PivotAdjuster : MonoBehaviour
{
    public Vector3 newPivotPosition;

    void Start()
    {
        Transform pivotTransform = new GameObject("Pivot").transform;
        pivotTransform.position = newPivotPosition;
        transform.SetParent(pivotTransform);
    }
}
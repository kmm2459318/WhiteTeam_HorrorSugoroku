using UnityEngine;
using System.Collections.Generic;

public class PlayerCloseMass : MonoBehaviour
{
    private List<Transform> objectsInTrigger = new List<Transform>();
    public string targetTag = "masu"; // 取得したいオブジェクトのタグ

    private void OnTriggerStay(Collider other)
    {
        // "masu" タグを持つオブジェクトのみリストに追加
        if (other.CompareTag(targetTag) && !objectsInTrigger.Contains(other.transform))
        {
            objectsInTrigger.Add(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // "masu" タグを持つオブジェクトのみリストから削除
        if (other.CompareTag(targetTag) && objectsInTrigger.Contains(other.transform))
        {
            objectsInTrigger.Remove(other.transform);
        }
    }

    public Transform GetClosestObject()
    {
        if (objectsInTrigger.Count == 0)
            return null;

        Transform closestObject = null;
        float minDistance = float.MaxValue;
        Vector3 currentPosition = transform.position;

        foreach (Transform obj in objectsInTrigger)
        {
            float distance = Vector3.Distance(currentPosition, obj.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestObject = obj;
            }
        }
        Debug.Log(closestObject);
        return closestObject;
    }
}

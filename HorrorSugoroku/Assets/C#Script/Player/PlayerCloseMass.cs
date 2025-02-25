using UnityEngine;
using System.Collections.Generic;

public class PlayerCloseMass : MonoBehaviour
{
    private List<Transform> objectsInTrigger = new List<Transform>();
    public string targetTag = "masu"; // �擾�������I�u�W�F�N�g�̃^�O

    private void OnTriggerStay(Collider other)
    {
        // "masu" �^�O�����I�u�W�F�N�g�̂݃��X�g�ɒǉ�
        if (other.CompareTag(targetTag) && !objectsInTrigger.Contains(other.transform))
        {
            objectsInTrigger.Add(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // "masu" �^�O�����I�u�W�F�N�g�̂݃��X�g����폜
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

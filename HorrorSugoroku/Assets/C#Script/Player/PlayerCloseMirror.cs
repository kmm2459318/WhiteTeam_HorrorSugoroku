using UnityEngine;

public class PlayerCloseMirror : MonoBehaviour
{
    public GameObject FindClosestMirror()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Mirror");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
}

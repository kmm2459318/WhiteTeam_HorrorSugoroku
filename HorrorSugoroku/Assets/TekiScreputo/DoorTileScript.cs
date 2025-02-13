using UnityEngine;

public class DoorTileScript : MonoBehaviour
{
    public MapCreator mapCreator;

    void OnDestroy()
    {
        if (mapCreator != null)
        {
            mapCreator.ReplaceTile(transform.position);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

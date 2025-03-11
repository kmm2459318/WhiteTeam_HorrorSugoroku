using UnityEngine;

public class EnemySkinChanger : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject newSkin0;
    public GameObject newSkin1;
    public GameObject newSkin2;
    public GameObject oldSkin0;
    public GameObject oldSkin1;
    public GameObject oldSkin2;

    void Start()
    {
        newSkin0.SetActive(false);
        newSkin1.SetActive(false);
        newSkin2.SetActive(false);
        oldSkin0.SetActive(true);
        oldSkin1.SetActive(true);
        oldSkin2.SetActive(true);
    }

    void Update()
    {
        if (gameManager.mapPiece == 3)
        {
            newSkin0.SetActive(true);
            newSkin1.SetActive(true);
            newSkin2.SetActive(true);
            oldSkin0.SetActive(false);
            oldSkin1.SetActive(false);
            oldSkin2.SetActive(false);
        }
    }
}

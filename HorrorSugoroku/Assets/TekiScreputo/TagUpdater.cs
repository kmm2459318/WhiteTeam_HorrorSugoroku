using UnityEngine;

public class TagUpdater : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // �S�Ă� Outline �R���|�[�l���g�����I�u�W�F�N�g���擾
        Outline[] outlineObjects = FindObjectsOfType<Outline>();

        int updatedCount = 0;

        foreach (Outline outline in outlineObjects)
        {
            GameObject obj = outline.gameObject;

            // �^�O�� "Untagged" �̏ꍇ�̂� "Item" �ɕύX
            if (obj.tag == "Untagged")
            {
                obj.tag = "Item";
                updatedCount++;
            }
        }

        Debug.Log($"�^�O�� 'Item' �ɕύX�����I�u�W�F�N�g��: {updatedCount}");
    }
}

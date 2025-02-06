using UnityEngine;

public class NeonTile : MonoBehaviour
{
    public Material[] neonMaterials; // �����̃l�I���}�e���A����ێ�����z��
    public Material transparentMaterial;
    public float emissionIntensity = 2.0f; // �C���X�y�N�^�[�Œ����\��Emission���x

    void Start()
    {
        // �^�C���̍쐬
        GameObject tile = GameObject.CreatePrimitive(PrimitiveType.Cube);
        tile.transform.localScale = new Vector3(1, 0.1f, 1);

        // ���̍쐬
        for (int i = 0; i < neonMaterials.Length; i++)
        {
            CreateEdge(tile, new Vector3(0.5f - i * 0.1f, 0.05f, 0), new Vector3(0.1f, 0.1f, 1), neonMaterials[i]);
        }

        // ���g�𓧖��ɂ���
        tile.GetComponent<Renderer>().material = transparentMaterial;
    }

    void CreateEdge(GameObject parent, Vector3 position, Vector3 scale, Material material)
    {
        GameObject edge = GameObject.CreatePrimitive(PrimitiveType.Cube);
        edge.transform.parent = parent.transform;
        edge.transform.localPosition = position;
        edge.transform.localScale = scale;
        edge.GetComponent<Renderer>().material = material;
    }
}
using UnityEngine;

public class ClickTest : MonoBehaviour
{
    bool n = false;

    void Update()
    {
        if (n)
        {
            Debug.Log("�G�ꂽ��I");
            n = false; // �t���O�����Z�b�g
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("�I�u�W�F�N�g���N���b�N����܂����I");
        n = true;
    }
}
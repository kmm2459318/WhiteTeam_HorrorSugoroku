using UnityEngine;

public class DiceSound : MonoBehaviour
{
    public AudioSource diceSound; // �T�C�R���̉�
    private bool hasRolled = false; // �T�C�R�����U��ꂽ���ǂ����̃t���O

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "PlayerDice")
        {
            Rigidbody diceRb = collision.gameObject.GetComponent<Rigidbody>();

            // ��x�ł���������t���O��true�ɂ���
            if (diceRb.linearVelocity.magnitude > 0.1f)
            {
                hasRolled = true;
            }

            // �U��ꂽ��̐ڐG�̂݉���炷
            if (hasRolled && !diceSound.isPlaying)
            {
                diceSound.Play();
            }
        }
    }
}

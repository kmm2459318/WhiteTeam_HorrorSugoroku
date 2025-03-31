using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class JumpScareAnimation : MonoBehaviour
{
    public Animator animator;
    public Button startButton;
    private string triggerName = "StartAttack";
    private float moveDuration = 0.25f;
    private Vector3 targetLocalPosition = Vector3.zero; // ���[�J�����W�ł̖ڕW�n�_
    private Vector3 initialLocalPosition;
    private float timeReset = 2f;
    private bool hasStarted = false;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Start()
    {
        initialLocalPosition = transform.localPosition;

        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        if (startButton != null)
        {
            startButton.onClick.AddListener(StartAnimation);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Loaded Scene: " + scene.name);

        if (scene.name == "JumpScare" && !hasStarted)
        {
            hasStarted = true;
            StartCoroutine(DelayStartAnimation());
        }
    }

    private IEnumerator DelayStartAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        StartAnimation();
    }

    public void StartAnimation()
    {
        Debug.Log("�A�j���[�V�����J�n");
        StartCoroutine(MoveObject());
    }

    private IEnumerator MoveObject()
    {
        float timeElapsed = 0f;
        while (timeElapsed < moveDuration)
        {
            transform.localPosition = Vector3.Lerp(initialLocalPosition, targetLocalPosition, timeElapsed / moveDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = targetLocalPosition;

        if (animator != null)
        {
            Debug.Log("�A�j���[�V�����̃g���K�[���Z�b�g: " + triggerName);
            animator.enabled = true;
            animator.SetTrigger(triggerName);
        }
        else
        {
            Debug.LogError("Animator���ݒ肳��Ă��܂���I");
        }

        yield return new WaitForSeconds(timeReset);
        ResetObject();
    }

    private void ResetObject()
    {
        transform.localPosition = initialLocalPosition;
        hasStarted = false;
    }
}

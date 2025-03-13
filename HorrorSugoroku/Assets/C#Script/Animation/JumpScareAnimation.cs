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
    private Vector3 targetLocalPosition = Vector3.zero; // ローカル座標での目標地点
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
        Debug.Log("アニメーション開始");
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
            Debug.Log("アニメーションのトリガーをセット: " + triggerName);
            animator.enabled = true;
            animator.SetTrigger(triggerName);
        }
        else
        {
            Debug.LogError("Animatorが設定されていません！");
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

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    [Header("Feedback")]
    [SerializeField] private float scaleMultiplier = 0.9f;
    [SerializeField] private float feedbackDuration = 0.1f;

    private Vector3 originalScale;

    private void Awake()
    {
        originalScale = transform.localScale;
    }

    public void VoltarAoMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void PlayFeedback()
    {
        StopAllCoroutines();
        StartCoroutine(FeedbackRoutine());
    }

    private IEnumerator FeedbackRoutine()
    {
        transform.localScale = originalScale * scaleMultiplier;

        yield return new WaitForSeconds(feedbackDuration);

        transform.localScale = originalScale;
    }
}
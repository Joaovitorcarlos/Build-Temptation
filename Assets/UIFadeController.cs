using UnityEngine;
using UnityEngine.UI;

public class UIFadeController : MonoBehaviour
{
    [Header("Referência")]
    [SerializeField] private Image targetImage;

    [Header("Objetos para ativar/desativar")]
    [SerializeField] private GameObject object1;
    [SerializeField] private GameObject object2;

    [Header("Fade In")]
    [SerializeField] private float fadeInStart = 5f;
    [SerializeField] private float fadeInDuration = 1f;

    [Header("Fade Out")]
    [SerializeField] private float fadeOutStart = 50f;
    [SerializeField] private float fadeOutDuration = 1f;

    private float timer;

    private bool objectsEnabled;
    private bool objectsDisabled;

    private void Start()
    {
        SetAlpha(0f);

        if (object1 != null)
            object1.SetActive(false);

        if (object2 != null)
            object2.SetActive(false);
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (!objectsEnabled && timer >= fadeInStart)
        {
            objectsEnabled = true;

            if (object1 != null)
                object1.SetActive(true);

            if (object2 != null)
                object2.SetActive(true);
        }

        if (!objectsDisabled && timer >= fadeOutStart)
        {
            objectsDisabled = true;

            if (object1 != null)
                object1.SetActive(false);

            if (object2 != null)
                object2.SetActive(false);
        }

        float alpha = 0f;

        if (timer >= fadeInStart && timer < fadeInStart + fadeInDuration)
        {
            alpha = Mathf.Lerp(
                0f,
                1f,
                (timer - fadeInStart) / fadeInDuration);
        }
        else if (timer >= fadeInStart + fadeInDuration && timer < fadeOutStart)
        {
            alpha = 1f;
        }
        else if (timer >= fadeOutStart && timer < fadeOutStart + fadeOutDuration)
        {
            alpha = Mathf.Lerp(
                1f,
                0f,
                (timer - fadeOutStart) / fadeOutDuration);
        }

        SetAlpha(alpha);
    }

    private void SetAlpha(float alpha)
    {
        Color color = targetImage.color;
        color.a = alpha;
        targetImage.color = color;
    }
}
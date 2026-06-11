using System.Collections;
using UnityEngine;

public class UIFeedbackManager : MonoBehaviour
{
    public static UIFeedbackManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void PlayFeedback(IEnumerator routine)
    {
        StartCoroutine(routine);
    }

    // Exemplo de feedback simples (pode adaptar)
    public IEnumerator ButtonClickFeedback(GameObject button)
    {
        if (button == null)
            yield break;

        button.transform.localScale = Vector3.one * 0.9f;
        yield return new WaitForSeconds(0.1f);
        button.transform.localScale = Vector3.one;
    }
}
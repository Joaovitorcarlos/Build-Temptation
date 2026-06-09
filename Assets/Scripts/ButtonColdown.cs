using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonCooldown : MonoBehaviour
{
    public Button botaoA;
    public Button botaoV;
    public float cooldown = 0.5f;

    private bool emCooldown = false;

    public void OnButtonClick()
    {
        // Ignora qualquer tentativa durante o cooldown
        if (emCooldown)
            return;

        emCooldown = true;

        Debug.Log("Botão clicado!");

        botaoA.interactable = false;
        botaoV.interactable = false;

        StartCoroutine(ReativarBotoes());
    }

    private IEnumerator ReativarBotoes()
    {
        yield return new WaitForSeconds(cooldown);

        botaoA.interactable = true;
        botaoV.interactable = true;

        emCooldown = false;
    }
}
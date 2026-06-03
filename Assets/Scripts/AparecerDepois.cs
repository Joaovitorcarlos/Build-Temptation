using System.Collections;
using TMPro;
using UnityEngine;

public class AparecerDepois : MonoBehaviour
{
    [Header("Referências")]
    public GameObject objeto;
    public TMP_Text pontosText;

    [Header("Pontuação")]
    public int pontos;

    private void Start()
    {
        if (objeto != null)
            objeto.SetActive(false);

        StartCoroutine(MostrarDepois());
    }

    IEnumerator MostrarDepois()
    {
        yield return new WaitForSeconds(50f);

        if (objeto != null)
            objeto.SetActive(true);
    }

    private void Update()
    {
        if (pontosText != null)
        {
            pontosText.text = "Pontos: " + pontos;
        }
    }

    public void AdicionarPontos(int quantidade)
    {
        pontos += quantidade;
    }
}
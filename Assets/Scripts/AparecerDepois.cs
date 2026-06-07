using System.Collections;
using TMPro;
using UnityEngine;

public class AparecerDepois : MonoBehaviour
{
    [Header("Referências")]
    public GameObject objeto;
    public TMP_Text pontosText;

    [Header("Configuração")]
    public float FimDaPartida = 50f;

    private Pontos pontos;

    private void Start()
    {
        pontos = FindFirstObjectByType<Pontos>();

        if (objeto != null)
            objeto.SetActive(false);

        StartCoroutine(MostrarDepois());
    }

    IEnumerator MostrarDepois()
    {
        yield return new WaitForSeconds(FimDaPartida);

        if (objeto != null)
            objeto.SetActive(true);

        AtualizarPontuacao();
    }

    private void Update()
    {
        if (objeto != null && objeto.activeSelf)
        {
            AtualizarPontuacao();
        }
    }

    void AtualizarPontuacao()
    {
        if (pontos != null && pontosText != null)
        {
            pontosText.text = "Pontuação Final: " + pontos.pontosMenager;
        }
    }
}
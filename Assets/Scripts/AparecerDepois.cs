using System.Collections;
using TMPro;
using UnityEngine;

public class AparecerDepois : MonoBehaviour
{
    [Header("Referências")]
    public GameObject objeto;
    public TMP_Text pontosText;
    public GameObject menu;

    [Header("Configuração")]
    public float FimDaPartida = 50f;

    private void Start()
    {
        if (objeto != null)
            objeto.SetActive(false);

        if (menu != null)
            menu.SetActive(false);

        StartCoroutine(MostrarDepois());
    }

    IEnumerator MostrarDepois()
    {
        yield return new WaitForSeconds(FimDaPartida);

        if (objeto != null)
            objeto.SetActive(true);

        if (menu != null)
            menu.SetActive(true);

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
        if (NetworkScoreManager.Instance == null)
            return;

        int p1 = NetworkScoreManager.Instance.Player1Points;
        int p2 = NetworkScoreManager.Instance.Player2Points;

        pontosText.text =
            "Player 1: " + p1 +
            "\nPlayer 2: " + p2 +
            "\nTotal: " + (p1 + p2);
    }
}
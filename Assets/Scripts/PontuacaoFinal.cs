using TMPro;
using UnityEngine;

public class PontuacaoFinal : MonoBehaviour
{
    public TMP_Text textoPontuacaoFinal;

    void Update()
    {
        if (NetworkScoreManager.Instance == null)
            return;

        textoPontuacaoFinal.text =
            "Pontuação Total: " +
            NetworkScoreManager.Instance.GetTotalPoints();
    }
}
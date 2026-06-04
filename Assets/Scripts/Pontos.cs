using TMPro;
using UnityEngine;

public class Pontos : MonoBehaviour
{
    public int pontosMenager;
    public TMP_Text pontosText;

    void Update()
    {
        pontosText.text = "Pontos: " + pontosMenager.ToString();
    }
}
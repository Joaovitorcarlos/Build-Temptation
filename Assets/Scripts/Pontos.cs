using TMPro;
using UnityEngine;

public class Pontos : MonoBehaviour
{
    public int pontosMenager;
    public TMP_Text pontosText;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip hitSound;

    private int ultimoValorPontos;

    void Start()
    {
        ultimoValorPontos = pontosMenager;
    }

    void Update()
    {
        pontosText.text = "Pontos: " + pontosMenager.ToString();

        if (pontosMenager > ultimoValorPontos)
        {
            audioSource.PlayOneShot(hitSound);
            ultimoValorPontos = pontosMenager;
        }
    }
}
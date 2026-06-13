using TMPro;
using UnityEngine;

public class Pontos : MonoBehaviour
{
    public int pontosMenager;

    [Header("UI")]
    public TMP_Text pontosText;
    public TMP_Text pontosText2;

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
        pontosText.text = "Pontos: " + pontosMenager;

        if (pontosText2 != null)
            pontosText2.text = "Pontos: " + pontosMenager;

        if (pontosMenager > ultimoValorPontos)
        {
            audioSource.PlayOneShot(hitSound);
            ultimoValorPontos = pontosMenager;
        }

        // AtualizarLugar();
    }

    // void AtualizarLugar()
    // {
    //     if (lugarText == null) return;
    //
    //     Pontos[] todosJogadores = FindObjectsOfType<Pontos>();
    //
    //     if (todosJogadores.Length < 2)
    //     {
    //         lugarText.text = "1º Lugar";
    //         return;
    //     }
    //
    //     bool estouGanhando = true;
    //     foreach (Pontos outro in todosJogadores)
    //     {
    //         if (outro == this) continue;
    //         if (outro.pontosMenager > pontosMenager)
    //         {
    //             estouGanhando = false;
    //             break;
    //         }
    //     }
    //
    //     lugarText.text = estouGanhando ? "1º Lugar" : "2º Lugar";
    // }
}
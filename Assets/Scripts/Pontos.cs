using TMPro;
using UnityEngine;
using Fusion;

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
        pontosText.text = "Pontos: " + pontosMenager;

        if (pontosMenager > ultimoValorPontos)
        {
            audioSource.PlayOneShot(hitSound);
            ultimoValorPontos = pontosMenager;
        }

        AtualizarRede();
    }

    void AtualizarRede()
    {
        if (NetworkScoreManager.Instance == null)
            return;

        NetworkRunner runner = FindObjectOfType<NetworkRunner>();
        if (runner == null)
            return;

        PlayerRef me = runner.LocalPlayer;

        // MESMA lógica da sua câmera
        int index = me.RawEncoded % 2;

        NetworkScoreManager.Instance.SetPoints(index, pontosMenager);
    }
}
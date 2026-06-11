using Fusion;
using UnityEngine;

public class ScoreNetwork : NetworkBehaviour
{
    public static ScoreNetwork Instance;

    [Networked] public int Player1Points { get; set; }
    [Networked] public int Player2Points { get; set; }

    private void Awake()
    {
        Instance = this;
    }

    public void AtualizarPontos(int playerId, int pontos)
    {
        if (playerId == 1)
            Player1Points = pontos;
        else
            Player2Points = pontos;
    }

    public int TotalPontos()
    {
        return Player1Points + Player2Points;
    }
}
using Fusion;
using UnityEngine;

public class NetworkScoreManager : NetworkBehaviour
{
    public static NetworkScoreManager Instance;

    [Networked] public int Player1Points { get; set; }
    [Networked] public int Player2Points { get; set; }

    public override void Spawned()
    {
        Instance = this;
    }

    public void SetPoints(int index, int points)
    {
        if (!Object.HasStateAuthority)
            return;

        if (index == 0)
            Player1Points = points;
        else
            Player2Points = points;
    }

    public int GetTotalPoints()
    {
        return Player1Points + Player2Points;
    }
}
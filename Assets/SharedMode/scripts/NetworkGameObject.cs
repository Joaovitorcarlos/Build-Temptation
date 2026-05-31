using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkGameManager : NetworkBehaviour
{
    [Networked]
    public bool MatchStarted { get; set; }

    private bool loaded;

    public override void FixedUpdateNetwork()
    {
        if (MatchStarted && !loaded)
        {
            loaded = true;

            LoadPlayerScene();
        }
    }

    void LoadPlayerScene()
    {
        PlayerRef primeiroJogador = default;
        bool encontrou = false;

        foreach (PlayerRef p in Runner.ActivePlayers)
        {
            if (!encontrou)
            {
                primeiroJogador = p;
                encontrou = true;
            }
        }

        if (Runner.LocalPlayer == primeiroJogador)
        {
            Debug.Log("PLAYER 1 -> Gameplay");
            SceneManager.LoadScene("Gameplay");
        }
        else
        {
            Debug.Log("PLAYER 2 -> Fase2");
            SceneManager.LoadScene("Fase2");
        }
    }
}
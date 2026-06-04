using Fusion;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkGameManager : NetworkBehaviour
{
    public override void Spawned()
    {
        Debug.Log("=================================");
        Debug.Log("[NetworkGameManager] SPAWNED");
        Debug.Log("LocalPlayer: " + Runner.LocalPlayer.PlayerId);
        Debug.Log("HasStateAuthority: " + Object.HasStateAuthority);
        Debug.Log("StateAuthority: " + Object.StateAuthority);
        Debug.Log("=================================");
    }

    public void RequestStartMatch()
    {
        Debug.Log("=================================");
        Debug.Log("[NetworkGameManager] RequestStartMatch");
        Debug.Log("Quem clicou: " + Runner.LocalPlayer.PlayerId);
        Debug.Log("=================================");

        RPC_StartGame();
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    private void RPC_StartGame()
    {
        Debug.Log("=================================");
        Debug.Log("[NetworkGameManager] RPC_StartGame");
        Debug.Log("LocalPlayer: " + Runner.LocalPlayer.PlayerId);
        Debug.Log("=================================");

        List<PlayerRef> players = new List<PlayerRef>();

        foreach (var p in Runner.ActivePlayers)
        {
            players.Add(p);
            Debug.Log("Player encontrado: " + p.PlayerId);
        }

        if (players.Count < 2)
        {
            Debug.LogWarning("Menos de 2 jogadores");
            return;
        }

        PlayerRef host = players[0];
        PlayerRef cliente = players[1];

        Debug.Log("Host: " + host.PlayerId);
        Debug.Log("Cliente: " + cliente.PlayerId);

        if (Runner.LocalPlayer == host)
        {
            Debug.Log("HOST -> FASE1");
            SceneManager.LoadScene("Fase1");
        }
        else if (Runner.LocalPlayer == cliente)
        {
            Debug.Log("CLIENTE -> FASE2");
            SceneManager.LoadScene("Fase2");
        }
    }
}
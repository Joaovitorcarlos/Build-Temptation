using Fusion;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkGameManager : NetworkBehaviour
{
    [Networked]
    public bool MatchStarted { get; set; }

    private bool loaded;

    public override void Spawned()
    {
        Debug.Log($"[NetworkGameManager] Spawned");
        Debug.Log($"LocalPlayer: {Runner.LocalPlayer.PlayerId}");
        Debug.Log($"HasStateAuthority: {Object.HasStateAuthority}");
        Debug.Log($"StateAuthority: {Object.StateAuthority}");
    }

    public void RequestStartMatch()
    {
        Debug.Log("RequestStartMatch");

        if (Object.HasStateAuthority)
        {
            Debug.Log("Sou StateAuthority");
            MatchStarted = true;
        }
        else
        {
            Debug.Log("Enviando RPC");
            RPC_RequestStartMatch();
        }
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    private void RPC_RequestStartMatch()
    {
        Debug.Log("RPC recebida");

        if (MatchStarted)
        {
            Debug.Log("Match já iniciada");
            return;
        }

        MatchStarted = true;
        Debug.Log("MatchStarted = true");
    }

    public override void FixedUpdateNetwork()
    {
        if (!MatchStarted)
            return;

        Debug.Log("MatchStarted detectado");

        if (loaded)
            return;

        loaded = true;

        Debug.Log("Chamando LoadPlayerScene");

        LoadPlayerScene();
    }

    private void LoadPlayerScene()
    {
        Debug.Log("LoadPlayerScene executado");

        List<PlayerRef> players = new List<PlayerRef>();

        foreach (var p in Runner.ActivePlayers)
        {
            players.Add(p);
            Debug.Log($"Player encontrado: {p.PlayerId}");
        }

        Debug.Log($"Total Players: {players.Count}");

        if (players.Count < 2)
        {
            Debug.LogWarning("Menos de 2 jogadores");
            return;
        }

        PlayerRef primeiro = players[0];
        PlayerRef segundo = players[1];

        Debug.Log($"Primeiro Player: {primeiro.PlayerId}");
        Debug.Log($"Segundo Player: {segundo.PlayerId}");
        Debug.Log($"Meu Player: {Runner.LocalPlayer.PlayerId}");

        if (Runner.LocalPlayer == primeiro)
        {
            Debug.Log("CARREGANDO FASE1");

            if (Application.CanStreamedLevelBeLoaded("Fase1"))
            {
                SceneManager.LoadScene("Fase1");
            }
            else
            {
                Debug.LogError("Fase1 não está no Build Settings");
            }
        }
        else if (Runner.LocalPlayer == segundo)
        {
            Debug.Log("CARREGANDO FASE2");

            if (Application.CanStreamedLevelBeLoaded("Fase2"))
            {
                SceneManager.LoadScene("Fase2");
            }
            else
            {
                Debug.LogError("Fase2 não está no Build Settings");
            }
        }
        else
        {
            Debug.LogWarning("Jogador não identificado");
        }
    }
}
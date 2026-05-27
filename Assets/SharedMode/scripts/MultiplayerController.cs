using Fusion;
using Fusion.Sockets;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MultiplayerController : MonoBehaviour
{
    [Header("UI")]
    public InputField nomeSala;          // LEGACY INPUTFIELD
    public Text erro;
    public Canvas telaEntrarSala;

    [Header("Player")]
    public GameObject playerPrefab;

    private NetworkRunner runner;

    //==================================================
    // CRIAR RUNNER
    //==================================================
    void CriarRunner()
    {
        if (runner != null)
            return;

        runner = gameObject.AddComponent<NetworkRunner>();
        runner.ProvideInput = true;
    }

    //==================================================
    // BOTÃO: CRIAR / ENTRAR SALA
    //==================================================
    public async void EntrarSala()
    {
        // segurança
        if (nomeSala == null)
        {
            Debug.LogError("nomeSala não atribuído no Inspector!");
            return;
        }

        if (string.IsNullOrEmpty(nomeSala.text))
        {
            erro.text = "Digite o nome da sala";
            return;
        }

        CriarRunner();

        var result = await runner.StartGame(new StartGameArgs()
        {
            GameMode = GameMode.Shared,
            SessionName = nomeSala.text,

            Scene = SceneRef.None,
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
        });

        if (!result.Ok)
        {
            erro.text = "Erro ao entrar na sala";
            return;
        }

        if (telaEntrarSala != null)
            telaEntrarSala.gameObject.SetActive(false);

        //==================================================
        // FLUXO SIMPLES DE CENA
        //==================================================

        if (runner.IsSharedModeMasterClient)
        {
            Debug.Log("HOST entrou → Fase1");
            SceneManager.LoadScene("Fase1");
        }
        else
        {
            Debug.Log("CLIENTE entrou → Fase2");
            SceneManager.LoadScene("Fase2");
        }
    }

    //==================================================
    // SPAWN DO PLAYER
    //==================================================
    public void OnSceneLoadDone(NetworkRunner runner)
    {
        if (runner == null)
            return;

        if (runner.LocalPlayer == PlayerRef.None)
            return;

        if (runner.GetPlayerObject(runner.LocalPlayer) != null)
            return;

        Vector3 spawnPos = runner.IsSharedModeMasterClient
            ? new Vector3(-5, 0, 0)
            : new Vector3(5, 0, 0);

        NetworkObject obj = runner.Spawn(
            playerPrefab,
            spawnPos,
            Quaternion.identity,
            runner.LocalPlayer
        );

        runner.SetPlayerObject(runner.LocalPlayer, obj);
    }
}
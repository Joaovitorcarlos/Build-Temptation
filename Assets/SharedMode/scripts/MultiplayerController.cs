using Fusion;
using Fusion.Sockets;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class MultiplayerController : MonoBehaviour, INetworkRunnerCallbacks
{
    [Header("UI")]
    public InputField nomeSala;
    public Text erro;
    public Canvas telaEntrarSala;

    [Header("Music")]
    public AudioSource musica;

    private NetworkRunner runner;

    private bool matchStarted;
    private bool musicStarted;

    private int localScore;
    private int localCombo;

    //==================================================
    // CREATE RUNNER
    //==================================================
    async Task CriarRunner()
    {
        if (runner != null)
        {
            await runner.Shutdown();
            Destroy(runner.gameObject);
            runner = null;
        }

        runner = gameObject.AddComponent<NetworkRunner>();
        runner.ProvideInput = true;
        runner.AddCallbacks(this);
    }

    //==================================================
    // ENTER ROOM (LOBBY)
    //==================================================
    public async void EntrarSala()
    {
        if (string.IsNullOrWhiteSpace(nomeSala.text))
        {
            erro.text = "Digite o nome da sala";
            return;
        }

        await CriarRunner();

        var result = await runner.StartGame(new StartGameArgs()
        {
            GameMode = GameMode.Shared,
            SessionName = nomeSala.text,

            // ✔ IMPORTANTE: NÃO CARREGA CENA AQUI
            Scene = SceneRef.None,

            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
        });

        if (!result.Ok)
        {
            erro.text = "Erro ao entrar na sala";
            Debug.LogError(result.ShutdownReason);
            return;
        }

        telaEntrarSala.gameObject.SetActive(false);

        Debug.Log("Entrou no lobby!");
    }

    //==================================================
    // PLAYER JOINED
    //==================================================
    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log("Player entrou: " + player);

        CheckStartGame();
    }

    //==================================================
    // PLAYER LEFT
    //==================================================
    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log("Player saiu: " + player);

        matchStarted = false;
    }

    //==================================================
    // CHECK LOBBY STATE
    //==================================================
    void CheckStartGame()
    {
        if (runner == null)
            return;

        if (matchStarted)
            return;

        int count = 0;

        foreach (var p in runner.ActivePlayers)
            count++;

        Debug.Log("Players na sala: " + count);

        if (count < 2)
            return;

        if (!runner.IsSharedModeMasterClient)
            return;

        matchStarted = true;

        Invoke(nameof(StartMatch), 2f);
    }

    //==================================================
    // START MATCH (CHANGE SCENE)
    //==================================================
    void StartMatch()
    {
        Debug.Log("2 players conectados → carregando gameplay");

        runner.LoadScene(SceneRef.FromIndex(1));
    }

    //==================================================
    // SCENE READY
    //==================================================
    public void OnSceneLoadDone(NetworkRunner runner)
    {
        Debug.Log("Cena carregada");

        StartMusic();
    }

    //==================================================
    // MUSIC
    //==================================================
    void StartMusic()
    {
        if (musicStarted)
            return;

        musicStarted = true;
        musica.Play();

        Debug.Log("Música iniciada");
    }

    //==================================================
    // UPDATE
    //==================================================
    void Update()
    {
        if (runner == null)
            return;

        HandleInput();
    }

    void HandleInput()
    {
        if (!musicStarted)
            return;

        var kb = Keyboard.current;
        if (kb == null)
            return;

        if (kb.aKey.wasPressedThisFrame) HitLane(0);
        if (kb.sKey.wasPressedThisFrame) HitLane(1);
        if (kb.dKey.wasPressedThisFrame) HitLane(2);
        if (kb.fKey.wasPressedThisFrame) HitLane(3);
    }

    void HitLane(int lane)
    {
        Debug.Log($"Player {runner.LocalPlayer} lane {lane}");

        if (CheckHit(lane))
        {
            localCombo++;
            localScore += 1000;
        }
        else
        {
            localCombo = 0;
        }
    }

    bool CheckHit(int lane)
    {
        return true;
    }

    //==================================================
    // REQUIRED CALLBACKS
    //==================================================
    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }
    public void OnConnectedToServer(NetworkRunner runner) { }
    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason) { }
    public void OnInput(NetworkRunner runner, NetworkInput input) { }
    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }
    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }
    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }
    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }
    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }
    public void OnSceneLoadStart(NetworkRunner runner) { }
    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, System.ArraySegment<byte> data) { }
    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress) { }
    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }     
}
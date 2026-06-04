using Fusion;
using Fusion.Sockets;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MultiplayerController : MonoBehaviour, INetworkRunnerCallbacks
{
    [Header("UI")]
    public InputField nomeSala;
    public Text erro;
    public Button btnIniciarPartida;

    [Header("Referencias")]
    public NetworkGameManager networkGameManager;

    private NetworkRunner runner;

    private static MultiplayerController instance;

    private async void Awake()
    {
        Debug.Log("MultiplayerController Awake -> " + gameObject.name);

        if (instance != null)
        {
            Debug.Log("MultiplayerController duplicado destruído");
            Destroy(gameObject);
            return;
        }

        instance = this;

        DontDestroyOnLoad(gameObject);

        runner = GetComponent<NetworkRunner>();

        if (runner == null)
            runner = gameObject.AddComponent<NetworkRunner>();

        runner.ProvideInput = true;
        runner.AddCallbacks(this);

        await Task.CompletedTask;
    }

    private void Start()
    {
        Debug.Log("MultiplayerController Start -> " + gameObject.name);

        if (btnIniciarPartida != null)
            btnIniciarPartida.gameObject.SetActive(false);

        if (erro != null)
            erro.text = "";
    }

    public async void EntrarSala()
    {
        if (string.IsNullOrWhiteSpace(nomeSala.text))
        {
            if (erro != null)
                erro.text = "Digite o nome da sala";

            return;
        }

        var sceneManager = GetComponent<NetworkSceneManagerDefault>();

        if (sceneManager == null)
            sceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>();

        var currentScene = SceneManager.GetActiveScene();

        StartGameResult result = await runner.StartGame(
            new StartGameArgs()
            {
                GameMode = GameMode.Shared,
                SessionName = nomeSala.text,
                Scene = SceneRef.FromIndex(currentScene.buildIndex),
                SceneManager = sceneManager
            });

        if (!result.Ok)
        {
            Debug.LogError(result.ShutdownReason);

            if (erro != null)
                erro.text = result.ShutdownReason.ToString();

            return;
        }

        Debug.Log("Entrou na sala " + nomeSala.text);
    }

    public void IniciarPartida()
    {
        Debug.Log("BOTAO CLICADO");

        if (networkGameManager == null)
            networkGameManager = FindObjectOfType<NetworkGameManager>(true);

        if (networkGameManager == null)
        {
            Debug.LogError("NetworkGameManager não encontrado");
            return;
        }

        networkGameManager.RequestStartMatch();
    }

    private void AtualizarLobby()
    {
        int count = 0;

        foreach (var p in runner.ActivePlayers)
            count++;

        Debug.Log("Players: " + count);

        if (btnIniciarPartida != null)
            btnIniciarPartida.gameObject.SetActive(count >= 2);
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log("Player entrou: " + player.PlayerId);
        AtualizarLobby();
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log("Player saiu: " + player.PlayerId);
        AtualizarLobby();
    }

    public void OnConnectedToServer(NetworkRunner runner)
    {
        Debug.Log("Conectado ao Photon");
    }

    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
    {
        Debug.Log("Desconectado: " + reason);
    }

    public void OnInput(NetworkRunner runner, NetworkInput input) { }
    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }
    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }
    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }
    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }
    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }
    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }
    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, System.ArraySegment<byte> data) { }
    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress) { }
    public void OnSceneLoadDone(NetworkRunner runner) { }
    public void OnSceneLoadStart(NetworkRunner runner) { }
    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
}
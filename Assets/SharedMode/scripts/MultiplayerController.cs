using Fusion;
using Fusion.Sockets;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MultiplayerController : MonoBehaviour, INetworkRunnerCallbacks
{
    [Header("UI")]
    public InputField nomeSala;
    public Text erro;
    public Button btnIniciarPartida;

    private NetworkRunner runner;
    private bool partidaIniciada;

    void Start()
    {
        SafeSetActive(btnIniciarPartida, false);
        SafeSetText(erro, "");
    }

    // =========================
    // SAFE HELPERS (ANTI CRASH)
    // =========================
    void SafeSetActive(GameObject obj, bool value)
    {
        if (obj != null)
            obj.SetActive(value);
    }

    void SafeSetActive(Component comp, bool value)
    {
        if (comp != null)
            comp.gameObject.SetActive(value);
    }

    void SafeSetText(Text t, string value)
    {
        if (t != null)
            t.text = value;
    }

    // =========================
    // RUNNER
    // =========================
    async Task CriarRunner()
    {
        if (runner != null)
        {
            await runner.Shutdown();
            Destroy(runner.gameObject);
            runner = null;
        }

        runner = gameObject.AddComponent<NetworkRunner>();

        if (runner == null)
        {
            Debug.LogError("Falha ao criar NetworkRunner");
            return;
        }

        runner.ProvideInput = true;
        runner.AddCallbacks(this);
    }

    // =========================
    // ENTRAR SALA
    // =========================
    public async void EntrarSala()
    {
        if (nomeSala == null || string.IsNullOrWhiteSpace(nomeSala.text))
        {
            SafeSetText(erro, "Digite o nome da sala");
            return;
        }

        SafeSetActive(btnIniciarPartida, false);

        await CriarRunner();

        if (runner == null)
        {
            SafeSetText(erro, "Runner não inicializado");
            return;
        }

        var result = await runner.StartGame(new StartGameArgs()
        {
            GameMode = GameMode.Shared,
            SessionName = nomeSala.text,
            Scene = SceneRef.None,
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
        });

        if (!result.Ok)
        {
            SafeSetText(erro, "Erro ao entrar na sala");
            Debug.LogError(result.ShutdownReason);
            return;
        }

        Debug.Log("Entrou na sala: " + nomeSala.text);
    }

    // =========================
    // PLAYERS
    // =========================
    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        AtualizarLobby();
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        AtualizarLobby();
    }

    // =========================
    // BOTÃO HOST
    // =========================
    void AtualizarLobby()
    {
        if (runner == null)
            return;

        int count = 0;

        foreach (var p in runner.ActivePlayers)
            count++;

        bool show = runner.IsSharedModeMasterClient && count >= 2;

        if (btnIniciarPartida != null)
            btnIniciarPartida.gameObject.SetActive(show);
    }

    // =========================
    // INICIAR PARTIDA
    // =========================
    public void IniciarPartida()
    {
        if (runner == null || !runner.IsSharedModeMasterClient)
            return;

        RPC_IniciarPartida();
    }

    // =========================
    // RPC GLOBAL
    // =========================
    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void RPC_IniciarPartida()
    {
        if (partidaIniciada)
            return;

        partidaIniciada = true;

        int indexLocal = -1;
        int index = 0;

        foreach (var p in runner.ActivePlayers)
        {
            if (p == runner.LocalPlayer)
                indexLocal = index;

            index++;
        }

        if (indexLocal == 0)
        {
            Debug.Log("Player 1 -> Fase1");
            SceneManager.LoadScene("Fase1");
        }
        else
        {
            Debug.Log("Player 2 -> Fase2");
            SceneManager.LoadScene("Fase2");
        }
    }

    // =========================
    // CALLBACKS OBRIGATÓRIOS
    // =========================
    public void OnConnectedToServer(NetworkRunner runner) { }
    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason) { }
    public void OnInput(NetworkRunner runner, NetworkInput input) { }
    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }
    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }
    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }
    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }
    public void OnSceneLoadStart(NetworkRunner runner) { }
    public void OnSceneLoadDone(NetworkRunner runner) { }
    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, System.ArraySegment<byte> data) { }
    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress) { }
    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }
    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }
}
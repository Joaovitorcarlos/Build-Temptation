using Fusion;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [Header("Câmeras")]
    public Camera cameraPlayer1;
    public Camera cameraPlayer2;

    private NetworkRunner runner;

    void Start()
    {
        runner = FindObjectOfType<NetworkRunner>();

        Invoke(nameof(SetupCamera), 0.2f);
    }

    void SetupCamera()
    {
        if (runner == null)
            return;

        PlayerRef me = runner.LocalPlayer;

        // Divide os players em 2 lados
        int index = me.RawEncoded % 2;

        if (cameraPlayer1 != null)
            cameraPlayer1.gameObject.SetActive(index == 0);

        if (cameraPlayer2 != null)
            cameraPlayer2.gameObject.SetActive(index == 1);

        Debug.Log($"Camera ativada para Player {index}");
    }
}
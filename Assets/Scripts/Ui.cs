using Fusion;
using UnityEngine;

public class CameraUIBinder : MonoBehaviour
{
    public Camera cameraPlayer1;
    public Camera cameraPlayer2;

    public Canvas canvasPlayer1;
    public Canvas canvasPlayer2;

    private NetworkRunner runner;

    void Start()
    {
        runner = FindObjectOfType<NetworkRunner>();

        Invoke(nameof(Setup), 0.2f);
    }

    void Setup()
    {
        if (runner == null)
            return;

        int index = runner.LocalPlayer.RawEncoded % 2;

        bool isP1 = index == 0;

        cameraPlayer1.gameObject.SetActive(isP1);
        cameraPlayer2.gameObject.SetActive(!isP1);

        canvasPlayer1.gameObject.SetActive(isP1);
        canvasPlayer2.gameObject.SetActive(!isP1);
    }
}
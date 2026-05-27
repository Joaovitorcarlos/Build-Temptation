using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class CameraController : NetworkBehaviour
{
    public Camera mainCamera;
    public override void Spawned()
    {
        if (HasInputAuthority)
        {
            mainCamera.enabled = true;
            mainCamera.GetComponent<AudioListener>().enabled = true;
        }
        else
        {
            mainCamera.enabled = false;
            mainCamera.GetComponent<AudioListener>().enabled = false;
        }
    }
    public override void Despawned(NetworkRunner runner, bool hasState)
    {
        mainCamera.gameObject.SetActive(false);
    }
}

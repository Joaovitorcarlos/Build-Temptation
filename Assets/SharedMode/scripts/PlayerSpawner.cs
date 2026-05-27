using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class PlayerSpawner : SimulationBehaviour, IPlayerJoined
{
    public GameObject playerPrefab;
    public void PlayerJoined(PlayerRef player)
    {
        if(player == Runner.LocalPlayer)
        {
            var objetoDaRede = Runner.Spawn(playerPrefab, 
                new Vector3(0,-1,0), 
                Quaternion.identity, 
                player);
            Runner.SetPlayerObject(player, objetoDaRede);
        }
    }
}

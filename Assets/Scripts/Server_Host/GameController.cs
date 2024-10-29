using Client;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class GameController : NetworkBehaviour
{
    public static GameController Instance;
    private void Awake()
    {
        Instance = this;
        readyCount = 0;
    }

    int readyCount = 0;
    public static ulong playerCount = 0;
    [ServerRpc(RequireOwnership = false)]
    public void RequestReadyServerRpc()
    {
        readyCount++;
        Debug.Log("Ready count:" + readyCount);
        if (readyCount == 5) return;
        if (readyCount == NetworkManager.ConnectedClients.Count)
        {
            Debug.Log("All are ready");
            StartGameClientRpc();
        }
    }
    [ServerRpc(RequireOwnership = false)]
    public void RequestUnreadyServerRpc()
    {
        readyCount--;
        Debug.Log("Ready count:" + readyCount);
    }
    [ClientRpc]
    public void StartGameClientRpc()
    {
        UI_Manager.Instance.StartCountdown();
    }
    public int acceptPause = 0;
    public static int timePause, playerRequestPauseID;
    public void RequestPauseGameServer(int secs)
    {
        RequestPauseGameServerRpc(secs, (int)PlayerController.ownerID);
    }
    [ServerRpc(RequireOwnership = false)]
    private void RequestPauseGameServerRpc(int secs, int idRequest)
    {
        acceptPause++;
        timePause = secs;
        playerRequestPauseID = idRequest;
        CheckPauseGame();
        UI_Manager.Instance.UpdateAcceptPauseCountClientRpc($"{acceptPause}/{NetworkManager.ConnectedClients.Count}");
    }
    [ServerRpc(RequireOwnership = false)]
    public void AcceptPauseServerRpc()
    {
        acceptPause++;
        CheckPauseGame();
        UI_Manager.Instance.UpdateAcceptPauseCountClientRpc($"{acceptPause}/{NetworkManager.ConnectedClients.Count}");
    }
    [ServerRpc(RequireOwnership = false)]
    public void RejectPauseServerRpc()
    {
        acceptPause = 0;
    }
    private void CheckPauseGame()
    {
        if (acceptPause != NetworkManager.ConnectedClients.Count)
        {
            UI_Manager.Instance.ShowRequestPauseGameClientRpc((ulong)playerRequestPauseID, timePause);
        }
        else
        {
            UI_Manager.Instance.PauseGameClientRpc(timePause);
            acceptPause = 0;
        }
    }
}

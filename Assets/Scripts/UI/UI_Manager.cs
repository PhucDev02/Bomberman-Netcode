using Client;
using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class UI_Manager : NetworkBehaviour
{
    public UI_Countdown countdown;
    public UI_LobbyClient lobby;
    public UI_PauseGame pauseUI;
    public UI_Pause pauseScreen;
    public UI_GameOver gameOverUI;
    public ChatManager chatManager;
    public static UI_Manager Instance;
    private void Awake()
    {
        Instance = this;
    }
    public void StartCountdown()
    {
        lobby.Close();
        countdown.StartCountDown();
    }
    public void SetLobby(int index)
    {
        lobby.SetMsg(index);
    }
    [ClientRpc]
    public void PauseGameClientRpc(int sec)
    {
        pauseScreen.Pause(sec);
        pauseUI.gameObject.SetActive(false);
    }
    [ClientRpc]
    internal void ShowRequestPauseGameClientRpc(ulong networkObjectId, int secs)
    {
        pauseUI.ShowRequest(networkObjectId, secs);
    }
    [ClientRpc]
    public void UpdateAcceptPauseCountClientRpc(string s)
    {
        pauseUI.UpdateAcceptCount(s);
    }
    public bool IsPause()
    {
        return pauseScreen.gameObject.activeInHierarchy
            || lobby.gameObject.activeInHierarchy
            || countdown.gameObject.activeInHierarchy
            || chatManager.chatFrame.activeInHierarchy;
    }
    public async void ShowGameOver(int winnerID)
    {
        await UniTask.Delay(1000);
        gameOverUI.Show(winnerID);
    }
}

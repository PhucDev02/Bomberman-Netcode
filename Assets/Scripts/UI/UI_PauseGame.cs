using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class UI_PauseGame : MonoBehaviour
{
    public TextMeshProUGUI msg,yesBtn;
    bool isSelected;
    public async void ShowRequest(ulong networkObjectId, int secs)
    {
        isSelected = false;
        if (PlayerController.ownerID == networkObjectId) return;
        gameObject.SetActive(true);
        msg.text = $"Player {networkObjectId} want to pause {secs} secs";
        await UniTask.Delay(10000);
        gameObject.SetActive(false);
    }
    public void AcceptPause()
    {
        if (isSelected) return;
        GameController.Instance.AcceptPauseServerRpc(); 
        isSelected = true;
    }
    public void RejectPause()
    {
        gameObject.SetActive(false);
        GameController.Instance.RejectPauseServerRpc();
    }
    public void UpdateAcceptCount(string s)
    {
        yesBtn.text = $"Yes({s})";
    }
}

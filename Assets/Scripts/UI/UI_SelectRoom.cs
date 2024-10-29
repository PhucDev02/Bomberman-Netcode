using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class UI_SelectRoom : UI_AbstractScreen
{
    public Transform board;
    public override void Open()
    {
        base.Open();
        board.DOScale(1, 0.5f).SetEase(Ease.OutBack);
    }
    public override void Close()
    {
        board.DOScale(0, 0.5f).SetEase(Ease.InBack);
        base.Close();
    }
    public override void Anim()
    {
    }

    public override void ResetAnim()
    {
        board.localScale = Vector3.zero;
    }
    public TextMeshProUGUI text;
    public void OnClickHost()
    {
        //text.text = Utility.GetLocalIPAddress().ToLower();
        LoadingSystem.Instance.LoadScene("Client",()=> NetworkManager.Singleton.StartHost());
    }
    public TMP_InputField input;
    public void OnChangeIP()
    {
        NetworkConfig.Instance.ChangeAddress(input.text);
    }
    public void OnClickJoin()
    {
        //NetworkManager.Singleton.StartClient();
        if(input.text=="")
            NetworkConfig.Instance.ChangeAddress("127.0.0.1");
        LoadingSystem.Instance.LoadScene("Client", () => NetworkManager.Singleton.StartClient());
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class UI_GameOver : UI_AbstractScreen
{
    public TextMeshProUGUI text;
    public void Show(int winnerId)
    {
        base.Open();
        if(winnerId == (int)PlayerController.ownerID)
        {
            text.text = "You are winner!";
        }
        else
        {
            text.text = $"You lost !\n Player {winnerId} is winner";
        }
    }
    public void Quit()
    {
        LoadingSystem.Instance.LoadScene("Home", 2.0f);
        NetworkManager.Singleton.Shutdown();
    }
    public override void Anim()
    {
    }

    public override void ResetAnim()
    {
    }

}

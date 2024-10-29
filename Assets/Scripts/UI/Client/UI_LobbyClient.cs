using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;

namespace Client
{
    public class UI_LobbyClient : UI_AbstractScreen
    {
        public static UI_LobbyClient Instance;
        private void Awake()
        {
            Instance = this;
        }
        public override void Anim()
        {
        }

        public override void ResetAnim()
        {
        }
        public TextMeshProUGUI msg;
        public GameObject ready, unready;
        public void SetMsg(int index)
        {
            msg.text = "You are player " + index;
            //msg.text = $"You are <color={Utility.GetPlayerColor(index).ToString().ToLower()}>{Utility.GetPlayerColor(index)}</color>";
            if (index==1)
            {
                msg.text += "\nHost IP: " + Utility.GetLocalIPAddress();
            }
        }
        public void OnClickReady()
        {
            if (GameManager.Instance.players.Count == 1) return;
            unready.SetActive(true);
            ready.SetActive(false);
            GameController.Instance.RequestReadyServerRpc();
        }
        public void OnClickUnready()
        {
            ready.SetActive(true);
            unready.SetActive(false);
            GameController.Instance.RequestUnreadyServerRpc();
        }
        public void QuitGame()
        {
            LoadingSystem.Instance.LoadScene("Home", 2.0f);
        }
    }
}
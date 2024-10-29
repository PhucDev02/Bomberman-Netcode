using TMPro;
using Unity.Netcode;
using UnityEngine;

public class ChatManager : NetworkBehaviour
{
    public GameObject chatFrame;
    public TMP_InputField inputField;
    public GameObject textTemplate;
    public Transform content;
    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            chatFrame.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!chatFrame.activeInHierarchy)
            {
                chatFrame.SetActive(true);
                inputField.Select();
                inputField.ActivateInputField();
                inputField.text = string.Empty;
            }
            else
            {
                Debug.Log("send msg");
                SendMessage();
            }
        }
        if (Input.GetKeyUp(KeyCode.O) && !chatFrame.activeInHierarchy)
        {
            inputField.text = "<sprite=0>";
            SendMessage();
        }
        if (Input.GetKeyUp(KeyCode.P) && !chatFrame.activeInHierarchy)
        {
            inputField.text = "<sprite=3>";
            SendMessage();
        }
        if (Input.GetKeyUp(KeyCode.K) && !chatFrame.activeInHierarchy)
        {
            inputField.text = "<sprite=1>";
            SendMessage();
        }
        if (Input.GetKeyUp(KeyCode.L) && !chatFrame.activeInHierarchy)
        {
            inputField.text = "<sprite=4>";
            SendMessage();
        }
    }
    public void OnClickEmoji(int index)
    {
        inputField.text += $"<sprite={index}> ";
    }
    public void SendMessage()
    {
        if (inputField.text == string.Empty)
        {
            chatFrame.SetActive(false);
            return;
        }

        RequestSendChatServerRpc((int)PlayerController.ownerID, inputField.text);
        inputField.text = string.Empty;
        chatFrame.SetActive(false);
    }
    [ServerRpc(RequireOwnership = false)]
    public void RequestSendChatServerRpc(int id, string msg, ServerRpcParams p = default)
    {
        GenerateMsgClientRpc(id, msg);
        //var obj = Instantiate(textTemplate, content);
        //obj.GetComponent<TextMeshProUGUI>().text = $"<color=yellow>Player {id} : </color>{msg}";
        //obj.GetComponent<NetworkObject>().Spawn();
    }
    [ClientRpc]
    public void GenerateMsgClientRpc(int id, string msg, ClientRpcParams p = default)
    {
        var obj = Instantiate(textTemplate, content);
        obj.GetComponent<TextMeshProUGUI>().text = $"<color=yellow>Player {id} : </color>{msg}";
    }
}

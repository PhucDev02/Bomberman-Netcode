using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Sirenix.OdinInspector;
using System;
using Client;

public class PlayerController : NetworkBehaviour
{
    public static ulong ownerID;
    public int idInGame;
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if (IsOwner)
        {
            ownerID = OwnerClientId+1;
            RequestIdentifyServerRpc(ownerID);
        }
        GameManager.Instance.AddPlayer(gameObject);
        idInGame = GameManager.Instance.players.Count;
    }

    [ServerRpc(RequireOwnership = false)]
    private void RequestIdentifyServerRpc(ulong index, ServerRpcParams param = default)
    {
        if (IsServer)
        {
            IdentifyNewPlayerClientRpc(index);
        }
    }
    [ClientRpc]
    private void IdentifyNewPlayerClientRpc(ulong index, ClientRpcParams param = default)
    {
        transform.position = Utility.GetInitPos((int)index);
        if (IsOwner && ownerID == index)
        {
            UI_Manager.Instance.SetLobby((int)index);
        }
    }
}

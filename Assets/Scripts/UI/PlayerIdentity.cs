using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerIdentity : NetworkBehaviour
{
    public GameObject obj;
    public override void OnNetworkSpawn()
    {
        obj.SetActive(IsOwner);
    }
}

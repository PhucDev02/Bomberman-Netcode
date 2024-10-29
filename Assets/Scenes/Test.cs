using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Test : MonoBehaviour
{
    [Button]
    public void Trigger()
    {
        DoSthServerRpc();   
    }
    [ServerRpc]
    public void DoSthServerRpc()
    {
        DoSthClientRpc();
    }
    [ClientRpc]
    public void DoSthClientRpc()
    {
        Debug.Log("hello");
    }
}

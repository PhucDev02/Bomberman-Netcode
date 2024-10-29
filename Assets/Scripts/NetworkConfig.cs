using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode.Transports.UTP;
using Sirenix.OdinInspector;

public class NetworkConfig : MonoBehaviour
{
    public static NetworkConfig Instance;
    public void Start()
    {
        Instance = this;
    }
    public UnityTransport transport;
    [Button]
    public void ChangeAddress(string s)
    {
        transport.ConnectionData.Address = s;
    }
}

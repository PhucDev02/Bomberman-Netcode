using Sirenix.OdinInspector;
using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading.Tasks;
using UnityEngine;

public class ListIP : MonoBehaviour
{
    public static string baseIP;
    [Button]
    public void Main()
    {
        //Debug.Log("Danh sách địa chỉ IP trong mạng LAN:");

        string localIPAddress = GetLocalIPAddress();
        Debug.Log(localIPAddress);

        string[] ipParts = localIPAddress.Split('.');
        baseIP = ipParts[0] + "." + ipParts[1] + "." + ipParts[2] + ".";

        for (int i = 1; i <= 255; i++)
        {
            string targetIP = baseIP + i;

            Task.Run(() => Ping(targetIP));
        }

    }
    [Button]
    public void Ping(int i)
    {
        Main();
        string targetIP = baseIP + i;
        Task.Run(() => Ping(targetIP));
        //Ping(targetIP);
    }
    // Lấy địa chỉ IP của máy local
    static string GetLocalIPAddress()
    {
        string hostName = Dns.GetHostName();
        IPHostEntry hostEntry = Dns.GetHostEntry(hostName);
        Debug.Log(hostEntry.AddressList.Length);
        foreach (IPAddress address in hostEntry.AddressList)
        {
            if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                return address.ToString();
            }
        }

        return null;
    }
    [Button]
    public void Test()
    {
        NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
        foreach (NetworkInterface adapter in nics)
        {
            foreach (var x in adapter.GetIPProperties().UnicastAddresses)
            {
                if (x.Address.AddressFamily == AddressFamily.InterNetwork && x.IsDnsEligible)
                {
                    Debug.Log( x.Address.ToString());
                }
            }
        }
    }
    static async void Ping(string ipAddress)
    {
        System.Net.NetworkInformation.Ping ping = new System.Net.NetworkInformation.Ping();
        try
        {
            PingReply reply = await ping.SendPingAsync(ipAddress);

            if (reply.Status == IPStatus.Success)
            {
                Debug.Log("IP có thể truy cập: " + ipAddress);
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning(e);
        }
    }
}

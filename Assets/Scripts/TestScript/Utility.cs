using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using UnityEngine;
using System.Runtime.Serialization;
using System.Net.Sockets;
using System.Net;
public class Utility
{
    
    public static byte[] Serialize(object obj)
    {
        byte[] serializedData;

        using (MemoryStream memoryStream = new MemoryStream())
        {
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(memoryStream, obj);
            serializedData = memoryStream.ToArray();
        }

        return serializedData;
    }
    public static T Deserialize<T>(byte[] data)
    {
        T deserializedObject;

        using (MemoryStream memoryStream = new MemoryStream(data))
        {
            IFormatter formatter = new BinaryFormatter();
            try
            {
                deserializedObject = (T)formatter.Deserialize(memoryStream);
            }
            catch (Exception)
            {
                throw;
            }
        }

        return deserializedObject;
    }
    public static Vector2 GetInitPos(int index)
    {
        switch (index)
        {
            case 1:
                return new Vector2(-6, 5);
            case 2:
                return new Vector2(6, 5);
            case 3:
                return new Vector2(-6, -5);
            case 4:
                return new Vector2(6, -5);
        }
        return Vector2.zero;
    }
    public static PlayerColor GetPlayerColor(int index)
    {
        if (index == 1)
            return PlayerColor.White;
        if (index == 2)
            return PlayerColor.Black;
        if (index == 3)
            return PlayerColor.Blue;
        return PlayerColor.Red;
    }
    public static string GetLocalIPAddress()
    {
        string localIP = string.Empty;

        IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());

        foreach (IPAddress ip in localIPs)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork) // IPV4
            {
                localIP = ip.ToString();
                break; 
            }
        }

        return localIP;
    }
}

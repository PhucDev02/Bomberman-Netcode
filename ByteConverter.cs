using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class ByteConverter : MonoBehaviour
{
    public static byte[] GetBytes(this T obj)
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
}

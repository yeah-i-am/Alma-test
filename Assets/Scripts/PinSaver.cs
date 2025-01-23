using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class PinSaver : MonoBehaviour
{
    public static void Save(string filePath)
    {
        Pin[] pins = FindObjectsByType<Pin>(FindObjectsSortMode.None);

        using (FileStream stream = File.Open(filePath, FileMode.Create))
        {
            BinaryFormatter formatter = new BinaryFormatter();

            foreach (Pin pin in pins)
            {
                formatter.Serialize(stream, pin.Data);
            }
        }
    }

    public static List<PinData> Load(string filePath)
    {
        List<PinData> pins = new List<PinData>();

        if (!File.Exists(filePath))
        {
            return pins;
        }

        using (FileStream stream = File.Open(filePath, FileMode.Open))
        {
            BinaryFormatter formatter = new BinaryFormatter();

            while (stream.Position != stream.Length)
            {
                pins.Add(formatter.Deserialize(stream) as PinData);
            }
        }

        return pins;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataPersistance 
{
    string path;
    public string Path { get => path; private set => path = value; }

    public DataPersistance()
    {
        Path = Application.persistentDataPath + "/Saved.txt";
        if (!File.Exists(Path))
        {
            Debug.Log(Path);
            File.Create(Path);
        }
    }

    public void WriteLine(string line)
    {
        StreamWriter writer = new StreamWriter(Path, false);
        Debug.Log("Line: " + line);
        writer.WriteLine(line);
        writer.Close();
    }

    public string ReadLine()
    {
        StreamReader reader = new StreamReader(Path);
        return reader.ReadLine();
    }
}

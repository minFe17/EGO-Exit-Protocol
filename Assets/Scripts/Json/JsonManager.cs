using System;
using System.IO;
using System.Text;
using UnityEngine;

public class JsonManager : MonoBehaviour
{
    // ╫л╠шео
    ReadData _readData = new ReadData();
    WriteData _writeData = new WriteData();

    StringBuilder _stringBuilder;

    string _memoryDataPath;

    public ReadData ReadData { get => _readData; }
    public WriteData WriteData { get => _writeData; }
    public string MemoryDataPath { get => _memoryDataPath; }

    void Awake()
    {
        Init();
    }

    public void Init()
    {
        CreateDataPath();
        _readData.Init(this);
        _writeData.Init(this);
    }

    void CreateDataPath()
    {
        if (_stringBuilder == null)
            _stringBuilder = new StringBuilder();
        CreateDataPath(out _memoryDataPath, "SaveMemoryData.json");
    }

    void CreateDataPath(out string path, string dataName)
    {
        _stringBuilder.Clear();
        _stringBuilder.Append(Application.persistentDataPath);
        _stringBuilder.Append(dataName);
        path = _stringBuilder.ToString();
    }

    void DestroyDataFile(string path)
    {
        if (CheckDataFile(path))
            File.Delete(path);
    }

    public bool CheckDataFile(string path)
    {
        return File.Exists(path);
    }

    public void DestroyDataFiles()
    {
        //DestroyDataFile();
    }
}
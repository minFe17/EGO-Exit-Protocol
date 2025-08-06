using System.IO;
using System.Text;
using UnityEngine;

public class JsonManager : MonoBehaviour
{
    // ╫л╠шео
    ReadData _readData = new ReadData();
    WriteData _writeData = new WriteData();

    string _memoryDataPath;
    string _loopDataPath;
    string _evidenceDataPath;

    public ReadData ReadData { get => _readData; }
    public WriteData WriteData { get => _writeData; }
    public string MemoryDataPath { get => _memoryDataPath; }
    public string LoopDataPath { get => _loopDataPath; }
    public string EvidenceDataPath { get => _evidenceDataPath; }

    public string MemoryDataName { get => "SaveMemoryData.json"; }
    public string LoopDataName { get => "SaveLoopData.json"; }
    public string EvidenceDataName { get => "SaveEvidenceData.json"; }

    public void Init()
    {
        CreateDataPath();
        _readData.Init(this);
        _writeData.Init(this);
    }

    void CreateDataPath()
    {
        CreateDataPath(out _memoryDataPath, MemoryDataName);
        CreateDataPath(out _loopDataPath, LoopDataName);
        CreateDataPath(out _evidenceDataPath, EvidenceDataName);
    }

    void CreateDataPath(out string path, string dataName)
    {
        Debug.Log("Application.persistentDataPath: " + Application.persistentDataPath);
        path = Path.Combine(Application.persistentDataPath, dataName);
    }

    public bool CheckDataFile(string path)
    {
        return File.Exists(path);
    }
}
using Steamworks;
using System.IO;
using UnityEngine;

public class SteamCloudManager : MonoBehaviour
{
    // ╫л╠шео
    public bool UploadFileToSteamCloud(string localFilePath, string cloudFileName)
    {
        if (!SteamManager.Initialized)
            return false;

        if (!File.Exists(localFilePath))
            return false;

        byte[] data = File.ReadAllBytes(localFilePath);
        return SteamRemoteStorage.FileWrite(cloudFileName, data, data.Length);
    }

    public byte[] ReadFileFromSteamCloud(string cloudFileName)
    {
        if (!SteamManager.Initialized)
            return null;
        if (!SteamRemoteStorage.FileExists(cloudFileName))
            return null;

        int size = SteamRemoteStorage.GetFileSize(cloudFileName);
        if (size == 0)
            return null;

        byte[] buffer = new byte[size];
        int read = SteamRemoteStorage.FileRead(cloudFileName, buffer, size);
        if (read != size)
            return null;

        return buffer;
    }
}
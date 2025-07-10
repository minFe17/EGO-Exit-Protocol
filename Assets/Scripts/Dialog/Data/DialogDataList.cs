using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogDataList
{
    [SerializeField] List<DialogData> _lines = new List<DialogData>();

    public List<DialogData> Lines { get => _lines; }
}
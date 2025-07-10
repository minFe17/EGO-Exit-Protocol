using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseDialogData
{
    [SerializeField] List<DialogData> _lines = new List<DialogData>();

    public List<DialogData> Lines { get => _lines; }
}
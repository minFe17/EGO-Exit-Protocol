using UnityEngine;

[System.Serializable]
public class ResearchJournalData
{
    [SerializeField] int _experimentalNumber;
    [SerializeField] int _textIndex;

    bool _isNewResearchJournal;

    public int ExperimentalNumber { get => _experimentalNumber; set => _experimentalNumber = value; }
    public int TextIndex { get => _textIndex; set => _textIndex = value; }
    public bool IsNewResearchJournal { get => _isNewResearchJournal; set => _isNewResearchJournal = value; }
}
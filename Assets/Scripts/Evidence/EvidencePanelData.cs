using UnityEngine;

[System.Serializable]
public class EvidencePanelData
{
    [SerializeField] int _evidenceType;
    [SerializeField] NullableVector3 _position;
    [SerializeField] ResearchJournalData _journalData;

    public EvidencePanelData(EEvidenceType evidenceType)
    {
        _evidenceType = (int)evidenceType;
        _position = new NullableVector3(null);
    }
    public EEvidenceType EvidentceType { get => (EEvidenceType)_evidenceType; }
    public Vector3? Position { get => _position.ToNullable(); set => _position = new NullableVector3(value); }
    public ResearchJournalData JournalData { get => _journalData; set => _journalData = value; }
}
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CurrentEvidenceList : MonoBehaviour
{
    // 데이터 싱글턴
    [SerializeField] List<EvidencePanelData> _currentEvidenceData = new List<EvidencePanelData>();

    public List<EvidencePanelData> CurrentEvidenceData { get => _currentEvidenceData; }

    public bool ContainsEvidenceData(EEvidenceType type)
    {
        if (type != EEvidenceType.ResearchJournal)
            return false;
        foreach(EvidencePanelData data in _currentEvidenceData)
        {
            if (data.EvidentceType == type)
                return true;
        }
        return false;
    }
}
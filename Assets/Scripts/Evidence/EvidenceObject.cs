using System.Collections.Generic;
using UnityEngine;
using Utils;

public class EvidenceObject : MonoBehaviour
{
    [SerializeField] List<EEvidenceType> _evidenceType;

    MediatorManager _mediatorManager;

    void Start()
    {
        _mediatorManager = GenericSingleton<MediatorManager>.Instance;
    }

    public void AddEvidence()
    {
        for (int i = 0; i < _evidenceType.Count; i++)
        {
            EvidencePanelData data = new EvidencePanelData(_evidenceType[i]);
            _mediatorManager.Notify(EMediatorEventType.CreateEvidencePanel, data);
        }
    }
}
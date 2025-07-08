using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EvidencePrefabLoad : PrefabLoadBase
{
    Dictionary<EEvidenceType, string> _evidecePrefabNameDict;
    Dictionary<EEvidenceType, GameObject> _evidencePrefabDict;

    public override void Init()
    {
        base.Init();
        _evidecePrefabNameDict = new Dictionary<EEvidenceType, string>
        {
            {EEvidenceType.Diploma, "Diploma" },
            {EEvidenceType.HumanCloneAgree, "HumanCloneAgree" },
            {EEvidenceType.HumanCloneDisagree, "HumanCloneDisagree" },
            {EEvidenceType.HumanCloneDisagree_Work, "HumanCloneDisagree_Work" },
            {EEvidenceType.ResearchJournal, "ResearchJournal" }
        };
    }

    public override async Task LoadPrefab()
    {
        if (_addressableManager == null)
            Init();
        _evidencePrefabDict = new Dictionary<EEvidenceType, GameObject>
        {
            {EEvidenceType.Diploma, await _addressableManager.GetAddressableAsset<GameObject>(_evidecePrefabNameDict[EEvidenceType.Diploma])},
            {EEvidenceType.HumanCloneAgree, await _addressableManager.GetAddressableAsset<GameObject>(_evidecePrefabNameDict[EEvidenceType.HumanCloneAgree])},
            {EEvidenceType.HumanCloneDisagree, await _addressableManager.GetAddressableAsset<GameObject>(_evidecePrefabNameDict[EEvidenceType.HumanCloneDisagree])},
            {EEvidenceType.HumanCloneDisagree_Work, await _addressableManager.GetAddressableAsset<GameObject>(_evidecePrefabNameDict[EEvidenceType.HumanCloneDisagree_Work])},
            {EEvidenceType.ResearchJournal, await _addressableManager.GetAddressableAsset<GameObject>(_evidecePrefabNameDict[EEvidenceType.ResearchJournal])},
        };
    }

    public override GameObject GetPrefab<TEnum>(TEnum type)
    {
        EEvidenceType key = (EEvidenceType)(object)type;
        return _evidencePrefabDict[key];
    }
}
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class EvidenceBoardUI : BoardUI, IMediatorEvent
{
    Stack<IMemento> _evidencePanelStack = new Stack<IMemento>();

    PrefabLoadBase _evidencePrefabLoad;

    #region BoardUI
    public override void Init()
    {
        base.Init();
        GenericSingleton<JsonManager>.Instance.ReadData.ReadEvidenceData();
        _evidencePrefabLoad = GenericSingleton<PrefabManager>.Instance.GetPrefabLoad(EPrefabType.Evidence);
        LoadEvidence();
        _mediatorManager.Register(EMediatorEventType.CreateEvidencePanel, this);
    }

    public override void Save(IMemento memoryMemento)
    {
        _evidencePanelStack.Push(memoryMemento);
    }

    public override void Restore()
    {
        if (_evidencePanelStack.Count > 0)
        {
            IMemento memory = _evidencePanelStack.Pop();
            memory.Restore();
        }
    }
    #endregion

    public void LoadEvidence()
    {
        List<EvidencePanelData> datas = DataSingleton<CurrentEvidenceList>.Instance.CurrentEvidenceData;
        for (int i = 0; i < datas.Count; i++)
        {
            GameObject temp = Instantiate(_evidencePrefabLoad.GetPrefab(datas[i].EvidentceType), this.gameObject.transform);
            temp.GetComponent<EvidencePanel>().Init(datas[i], this, _yBoundary);
            if (datas[i].EvidentceType == EEvidenceType.ResearchJournal)
                temp.GetComponent<ResearchJournal>().Init(datas[i].JournalData);
        }
    }

    #region Interface
    void IMediatorEvent.HandleEvent(object data)
    {
        EvidencePanelData evidencePanelData = (EvidencePanelData)data;
        if (DataSingleton<CurrentEvidenceList>.Instance.ContainsEvidenceData(evidencePanelData.EvidentceType))
            return;
        if (evidencePanelData.Position == null)
            evidencePanelData.Position = RandomPosition();

        GameObject temp = Instantiate(_evidencePrefabLoad.GetPrefab(evidencePanelData.EvidentceType), this.gameObject.transform);
        temp.GetComponent<EvidencePanel>().Init(evidencePanelData, this, _yBoundary);

        if(evidencePanelData.EvidentceType == EEvidenceType.ResearchJournal)
        {
            evidencePanelData.JournalData = new ResearchJournalData();
            evidencePanelData.JournalData.IsNewResearchJournal = true;
            temp.GetComponent<ResearchJournal>().Init(evidencePanelData.JournalData);
        }

        DataSingleton<CurrentEvidenceList>.Instance.CurrentEvidenceData.Add(evidencePanelData);
        GenericSingleton<JsonManager>.Instance.WriteData.WriteEvidenceData();
        GenericSingleton<AchievementManager>.Instance.AddStatAndCheckAchievement(EStatID.EVIDENCE_COUNT, 1, EAchievementID.ACH_EVIDENCE_COLLECT, 5);
    }
    #endregion
}
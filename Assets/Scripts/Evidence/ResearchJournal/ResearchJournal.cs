using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class ResearchJournal : MonoBehaviour
{
    [SerializeField] List<string> _textList;
    [SerializeField] Text _experimentalNumberText;
    [SerializeField] Text _recordText;

    ResearchJournalData _data;

    public void Init(ResearchJournalData data)
    {
        _data = data;
        ShowExperimentalNumber();
        ShowRecord();
    }

    void ShowExperimentalNumber()
    {
        if(_data.IsNewResearchJournal)
        {
            int number = DataSingleton<LoopData>.Instance.LoopCount - 1;
            _data.ExperimentalNumber = number;
        }
        _experimentalNumberText.text = string.Format("CL-219-{0:D3}", _data.ExperimentalNumber);
    }

    void ShowRecord()
    {
        if(_data.IsNewResearchJournal)
        {
            int randomIndex = Random.Range(0, _textList.Count);
            _data.TextIndex = randomIndex;
        }
        _recordText.text = _textList[_data.TextIndex];
    }
}
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class MemoryBoardUI : BoardUI, IMediatorEvent
{
    Stack<IMemento> _memoryPanelStack = new Stack<IMemento>();

    PrefabLoadBase _uIPrefabLoad;

    #region BoardUI
    public override void Init()
    {
        base.Init();
        _mediatorManager.Register(EMediatorEventType.CreateMemoryPanel, this);
        _uIPrefabLoad = GenericSingleton<PrefabManager>.Instance.GetPrefabLoad(EPrefabType.UI);
    }

    public override void Save(IMemento memoryMemento)
    {
        _memoryPanelStack.Push(memoryMemento);
    }

    public override void Restore()
    {
        if (_memoryPanelStack.Count > 0)
        {
            IMemento memory = _memoryPanelStack.Pop();
            memory.Restore();
        }
    }
    #endregion

    #region Interface
    void IMediatorEvent.HandleEvent(object data)
    {
        MemoryPanelData memoryPanelData = (MemoryPanelData)data;
        if (memoryPanelData.Position == null)
            memoryPanelData.Position = RandomPosition();

        GameObject temp = Instantiate(_uIPrefabLoad.GetPrefab(EUIPrefabType.MemoryPanel), this.gameObject.transform);
        temp.GetComponent<MemoryPanel>().Init(memoryPanelData, this, _yBoundary);
        GenericSingleton<JsonManager>.Instance.WriteData.WriteCurrentMemoryData();
    }
    #endregion
}
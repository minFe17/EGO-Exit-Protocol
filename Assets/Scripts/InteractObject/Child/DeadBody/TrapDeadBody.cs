using UnityEngine;
using Utils;

public class TrapDeadBody : DeadBody
{
    [SerializeField] MemoryObject _memoryObject;
    [SerializeField] Vector3 _researcherSpawnPos;

    public override void Interact()
    {
        _memoryObject.AddMemory();
        GenericSingleton<MediatorManager>.Instance.Notify(EMediatorEventType.SpawnResearcher, _researcherSpawnPos);
    }
}
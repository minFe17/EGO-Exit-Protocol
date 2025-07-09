using UnityEngine;
using Utils;

public class BasementFirstFloorTile : MonoBehaviour
{
    [SerializeField] MemoryObject _memoryObject;
    [SerializeField] Vector3 _researcherSpawnPos;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if (!GenericSingleton<PlayerManager>.Instance.HavePhone())
                return;
            _memoryObject.AddMemory();
            GenericSingleton<MediatorManager>.Instance.Notify(EMediatorEventType.SpawnResearcher, _researcherSpawnPos);
        }
    }
}
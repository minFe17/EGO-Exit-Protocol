using System.Threading.Tasks;
using UnityEngine;
using Utils;

public abstract class PrefabLoadBase : MonoBehaviour
{
    protected AddressableManager _addressableManager;

    public virtual void Init()
    {
        if (_addressableManager == null)
            _addressableManager = GenericSingleton<AddressableManager>.Instance;
    }

    public abstract Task LoadPrefab();
}
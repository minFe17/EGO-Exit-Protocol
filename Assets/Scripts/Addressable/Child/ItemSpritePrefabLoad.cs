using System.Threading.Tasks;
using UnityEngine.U2D;

public class ItemSpritePrefabLoad : PrefabLoadBase
{
    SpriteAtlas _itemIconSprite;
    string _name;

    public override void Init()
    {
        base.Init();
        _name = "ItemIcon";
    }

    public override async Task LoadPrefab()
    {
        if (_addressableManager == null)
            Init();
        _itemIconSprite = await _addressableManager.GetAddressableAsset<SpriteAtlas>(_name);
    }

    public override T GetPrefab<T>()
    {
        if (typeof(T) == typeof(SpriteAtlas))
            return (T)(object)_itemIconSprite;
        return default(T);
    }
}

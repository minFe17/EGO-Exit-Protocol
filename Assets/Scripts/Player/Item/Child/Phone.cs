using Utils;

public class Phone : ItemBase
{
    public override void Use()
    {
        GenericSingleton<MediatorManager>.Instance.Notify(EMediatorEventType.UsePhone);
    }

    protected override void Init()
    {
        _itemType = EItemType.Phone;
        base.Init();
    }
}
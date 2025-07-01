public class Phone : ItemBase
{
    public override void Use()
    {

    }

    protected override void Init()
    {
        _itemType = EItemType.Phone;
        base.Init();
    }
}
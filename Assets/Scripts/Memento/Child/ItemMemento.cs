using UnityEngine;

public class ItemMemento : MonoBehaviour
{
    IItemHolder _itemHolder;
    public IItemHolder ItemHolder { get=> _itemHolder; set=> _itemHolder = value; }
}
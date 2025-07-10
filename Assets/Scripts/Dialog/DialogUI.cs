using UnityEngine;
using UnityEngine.UI;
using Utils;

public class DialogUI : MonoBehaviour
{
    [SerializeField] EDialogCharacterType _characterType;
    [SerializeField] Text _text;

    public void Init()
    {
        GenericSingleton<DialogManager>.Instance.SetDialogUI(_characterType, this);
    }

    public void ShowDialog(string dialog)
    {
        _text.text = dialog;
    }
}

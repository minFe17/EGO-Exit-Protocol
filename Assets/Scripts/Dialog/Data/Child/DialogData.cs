using System;
using UnityEngine;

[System.Serializable]
public class DialogData
{
    [SerializeField] string _text;
    [SerializeField] string _characterTypeText;

    EDialogCharacterType _characterType;

    public string Text { get => _text; }
    public EDialogCharacterType CharacterType { get => _characterType; }

    public void Init()
    {
        if (!Enum.TryParse(_characterTypeText, out _characterType))
            _characterType = EDialogCharacterType.Player;
    }
}
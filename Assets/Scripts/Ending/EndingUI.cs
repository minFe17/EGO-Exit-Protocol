using System.Collections.Generic;
using UnityEngine;
using Utils;

public class EndingUI : MonoBehaviour
{
    [SerializeField] List<GameObject> _endingPanel;

    void Start()
    {
        EEndingType endingType = GenericSingleton<EndingManager>.Instance.EndingType;
        _endingPanel[(int)endingType].SetActive(true);
    }
}
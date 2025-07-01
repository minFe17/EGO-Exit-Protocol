using System.Collections.Generic;
using UnityEngine;

public class Dial : MonoBehaviour
{
    [SerializeField] List<DialNumber> _dialNumberList = new List<DialNumber>();

    public void CheckTargetNumber()
    {
        for (int i = 0; i < _dialNumberList.Count; i++)
        {
            if (!_dialNumberList[i].IsEqualTargetNumber)
                return;
        }
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
}
using UnityEngine;

public class Credit : MonoBehaviour
{
    [SerializeField] Animator _animator;

    #region Button
    public void OnCredit()
    {
        _animator.SetTrigger("doOnCredit");
    }

    public void OffCredit()
    {
        _animator.SetTrigger("doOffCredit");
    }
    #endregion
}
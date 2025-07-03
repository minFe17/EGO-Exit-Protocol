using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingDoor : DoorBase
{
    [SerializeField] Animator _animator;

    #region DoorBase
    protected override void InteractDoor()
    {
        CheckOpen();
    }
    #endregion

    void CheckOpen()
    {
        if (!_collider.isTrigger)
            Open();
        else
            OnInteract();
    }

    void Open()
    {
        if (_animator.GetBool("isOpen"))
            return;
        _animator.SetBool("isOpen", true);
    }

    public override void OnInteract()
    {
        // ���̵� ����?
        // ����ó��
        // �� �̵��̶� �ǳ� �� �����
        // �� ��ȯ ���߱�(�ð� ����)
        // ������ �̺�Ʈ �� ����
        SceneManager.LoadScene("EndingScene");
    }

    #region Animation Event
    void EndOpen()
    {
        // ���ȴٴ� ��� ó��?
        _collider.isTrigger = true;
    }
    #endregion

    #region Interface
    public override void OnLoopEvent()
    {
        base.OnLoopEvent();
        if (_animator != null)
            _animator.SetBool("isOpen", false);
    }
    #endregion
}

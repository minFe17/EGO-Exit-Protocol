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
        // 페이드 연출?
        // 엔딩처리
        // 씬 이동이랑 판넬 뭐 띄울지
        // 적 소환 멈추기(시간 정지)
        // 중재자 이벤트 다 삭제
        SceneManager.LoadScene("EndingScene");
    }

    #region Animation Event
    void EndOpen()
    {
        // 열렸다는 대사 처리?
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

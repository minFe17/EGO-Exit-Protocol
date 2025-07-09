using UnityEngine;

public class NewsPaperDesk : Desk
{
    [SerializeField] EvidenceObject _evidenceObject;

    public override void InteractEvent()
    {
        _evidenceObject.AddEvidence();
    }
}
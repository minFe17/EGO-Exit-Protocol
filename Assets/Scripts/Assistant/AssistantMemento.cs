using UnityEngine;

public class AssistantMemento : MonoBehaviour
{
    Vector3 _assistantPosition;
    Vector3 _assistantScale;
    EAssistantStateType _assistantType;

    public Vector3 AssistantPositon { get => _assistantPosition; set => _assistantPosition = value; }
    public Vector3 AssistantScale { get => _assistantScale; set => _assistantScale = value; }
    public EAssistantStateType AssistantType { get => _assistantType; set => _assistantType = value; }
}
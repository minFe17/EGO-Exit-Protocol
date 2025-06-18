using UnityEngine;

public class AssistantMemento : MonoBehaviour
{
    Vector3 _assistantPosition = new Vector3(-69.3f, -2.4f, 0);
    Vector3 _assistantScale;
    EAssistantStateType _assistantType;

    public Vector3 AssistantPositon { get => _assistantPosition;}
    public Vector3 AssistantScale { get => _assistantScale; set => _assistantScale = value; }
    public EAssistantStateType AssistantType { get => _assistantType; set => _assistantType = value; }
}
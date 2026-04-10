using UnityEngine;

public abstract class Agent : MonoBehaviour
{
    protected AgentMover _agentMover;
    protected AgentRenderer _agentRenderer;

    protected virtual void Awake()
    {
        _agentMover = GetComponentInChildren<AgentMover>();
        _agentRenderer = GetComponentInChildren<AgentRenderer>();
    }
}

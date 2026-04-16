using UnityEngine;

public class AgentRenderer : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetFloatParam(int paramHash, float value)
    {
        _animator.SetFloat(paramHash, value);
    }

    public void SetIntegerParam(int paramHash, int value)
    {
        _animator.SetInteger(paramHash, value);
    }

    public void SetBoolParam(int paramHash, bool value)
    {
        _animator.SetBool(paramHash, value);

    }
}

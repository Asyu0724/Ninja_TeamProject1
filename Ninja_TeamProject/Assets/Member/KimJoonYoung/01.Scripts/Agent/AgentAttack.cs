using Microsoft.Win32.SafeHandles;
using UnityEditor.AdaptivePerformance.Editor;
using UnityEngine;

public class AgentAttack : MonoBehaviour
{

    // 오버랩
    [Header("OverLab")]
    public Vector2 boxSize;
    public Vector2 offset;
    private Vector2 offsetMinus;
    private Vector2 offsetPlus;

    private void Awake()
    {
        offsetPlus.x = offset.x;
        offsetMinus.x = offset.x * -1;
    }
    public void Flip(float moveDir)
    {
        if (moveDir >= 0)
        {
            offset.x = offsetPlus.x;
        }
        else if (moveDir < 0)
        {
            offset = offsetMinus;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position + (Vector3)offset,boxSize);
    }
}

using Microsoft.Win32.SafeHandles;
using UnityEditor.AdaptivePerformance.Editor;
using UnityEngine;

public class AgentAttack : MonoBehaviour
{

    // 오버랩
    [Header("OverLab")]
    public Vector2 boxSize;
    private Vector2 firstboxSize;

    public Vector2 offset;
    private Vector2 firstOffset;
    private Vector2 offsetMinus;
    private Vector2 offsetPlus;

    private void Awake()
    {
        firstboxSize = boxSize;
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

    public void SkillBoxSize(Vector2 Size)
    {
        boxSize = Size;
    }

    public void FirstBoxSize()
    {
        boxSize = firstboxSize;
    }
    public void SkillOffset(Vector2 Offset)
    {
        if (offset.x >= 0)
        {
            offset.x = Offset.x;
            offset.y = Offset.y;
        }
        else if (offset.x < 0)
        {
            offset.x = -Offset.x;
            offset.y = Offset.y;
        }
    }

    public void FirstOffset()
    {
        offset = firstOffset;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position + (Vector3)offset,boxSize);
    }
}

using Microsoft.Win32.SafeHandles;
using UnityEditor.AdaptivePerformance.Editor;
using UnityEngine;

public class AgentAttack : MonoBehaviour
{

    // 오버랩
    [Header("OverLab")]
    public Vector2 boxSize;
    public Vector2 offset;
    private bool isAttacked;

    public void Flip(float moveDir)
    {
        offset.x = moveDir;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position + (Vector3)offset,boxSize);
    }
}

using UnityEngine;

public class EnemyRenderer : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriter;
    [SerializeField] private Transform playerTrm;
    private Vector2 moveDir;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();
    }
    private void FixedUpdate()
    {
        moveDir = playerTrm.position - transform.position;
        moveDir.Normalize();
    }
    private void LateUpdate()
    {
        animator.SetFloat("MoveX", moveDir.x);
    }
}

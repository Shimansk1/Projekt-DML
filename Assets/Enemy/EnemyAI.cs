using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float detectionRadius = 50f;
    public float attackRadius = 4f;
    public float moveSpeed = 5.5f;

    public int attackDamage = 10;
    public float attackCooldown = 2f;

    private float lastAttackTime = -Mathf.Infinity;

    private NavMeshAgent agent;
    private Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (agent != null)
            agent.speed = moveSpeed;

        animator = GetComponentInChildren<Animator>();
    }


    void Update()
    {
        if (player == null || agent == null || !agent.isOnNavMesh) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= detectionRadius)
        {
            if (distance > attackRadius)
            {
                agent.isStopped = false;
                agent.SetDestination(player.position);

                animator.SetBool("IsRunning", true);
                animator.SetBool("IsFighting", false);
            }
            else
            {
                agent.ResetPath();
                agent.isStopped = true;

                animator.SetBool("IsRunning", false);
                animator.SetBool("IsFighting", true);

                TryAttack();
            }
        }
        else
        {
            if (!agent.pathPending)
            {
                agent.ResetPath();
                agent.isStopped = true;

                animator.SetBool("IsRunning", false);
                animator.SetBool("IsFighting", false);
            }
        }
    }


    void TryAttack()
    {
        if (Time.time - lastAttackTime < attackCooldown) return;

        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null && !playerHealth.isDead)
        {
            playerHealth.TakeDamage(attackDamage);
            Debug.Log($"{gameObject.name} kousnul hráče za {attackDamage} HP!");
            lastAttackTime = Time.time;
        }
    }

    void SetAnimationState(bool isIdle, bool isRunning, bool isFighting)
    {
        if (animator == null) return;

        animator.SetBool("IsIdle", isIdle);
        animator.SetBool("IsRunning", isRunning);
        animator.SetBool("IsFighting", isFighting);
    }
}

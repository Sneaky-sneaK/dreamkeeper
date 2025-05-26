using UnityEngine;

public class ShadowEnemyAI : MonoBehaviour
{
    private enum EnemyState { Idle, Wander, Chase }
    private EnemyState currentState = EnemyState.Idle;

    private float stateTimer = 0f;
    private float wanderDuration = 2f;
    private float idleDuration = 2f;

    private Vector3 wanderDirection;

    public Transform player;
    public float moveSpeed = 3f;
    public float stopDistance = 1.5f;
    public bool facePlayer = true;
    public float distanceToChasePlayer = 20;

    [Header("Health Settings")]
    public int maxHealth = 3;
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;

        if (player == null) {
            player = FindFirstObjectByType<PlayerMovement>().gameObject.transform;
        }
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= distanceToChasePlayer && currentState != EnemyState.Chase)
        {
            // Switch to chase
            currentState = EnemyState.Chase;
        }
        else if (distance > distanceToChasePlayer && currentState == EnemyState.Chase)
        {
            // If player is out of range, go back to wandering
            PickNextState();
        }

        switch (currentState)
        {
            case EnemyState.Chase:
                ChasePlayer();
                break;
            case EnemyState.Wander:
                Wander();
                break;
            case EnemyState.Idle:
                Idle();
                break;
        }
    }

    void ChasePlayer()
    {
        if (Vector3.Distance(transform.position, player.position) > stopDistance)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;

            if (facePlayer)
            {
                Vector3 lookDirection = new Vector3(player.position.x, transform.position.y, player.position.z);
                transform.LookAt(lookDirection);
            }
        }
    }
    void Wander()
    {
        transform.position += wanderDirection * moveSpeed * Time.deltaTime;

        stateTimer -= Time.deltaTime;
        if (stateTimer <= 0f)
        {
            PickNextState();
        }
    }

    void Idle()
    {
        stateTimer -= Time.deltaTime;
        if (stateTimer <= 0f)
        {
            PickNextState();
        }
    }

    void PickNextState()
    {
        // 50% chance to idle, 50% to wander
        if (Random.value < 0.5f)
        {
            currentState = EnemyState.Idle;
            stateTimer = idleDuration + Random.Range(-0.5f, 0.5f);
        }
        else
        {
            currentState = EnemyState.Wander;
            stateTimer = wanderDuration + Random.Range(-0.5f, 0.5f);
            float angle = Random.Range(0f, 360f);
            wanderDirection = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)).normalized;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(1);
            Destroy(collision.gameObject); // Destroy the bullet on impact
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        Debug.Log($"Enemy hit! Remaining health: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Enemy died!");
        Destroy(gameObject); // You can replace this with a death animation/effect
    }
}
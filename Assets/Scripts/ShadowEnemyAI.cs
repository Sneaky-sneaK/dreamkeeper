using UnityEngine;

public class ShadowEnemyAI : MonoBehaviour
{
    private enum EnemyState { Idle, Wander, Chase, Attack, Cooldown }
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

    [Header("Attacking")]
    public float attackCooldown = 2f;
    private float cooldownTimer = 0f;
    public float attackRange = 1.5f; // How close enemy must be to attack
    public int damage = 1; // How much damage enemy does
    private bool hasAttacked = false;

    [Header("Health Settings")]
    public int maxHealth = 3;
    private int currentHealth;

    public EnemySpawnManager spawner;

    private void Start()
    {
        currentHealth = maxHealth;

        if (player == null) {
            player = FindFirstObjectByType<PlayerMovement>().gameObject.transform;
        }

        spawner = FindFirstObjectByType<EnemySpawnManager>();
    }

    void Update()
    {
        if (player == null) return;
        if (PlayerHealth.IsDead) return;

        float distance = Vector3.Distance(transform.position, player.position);

        bool playerIsInSafeZone = LightMeter.isInSafeZone;

        if (playerIsInSafeZone)
        {
            // Player is safe – stop chasing
            if (currentState != EnemyState.Idle && currentState != EnemyState.Wander)
            {
                currentState = EnemyState.Idle;
                return;
            }
        }

        // If not already attacking or cooling down and player gets close, start chasing
        else if (currentState != EnemyState.Chase && currentState != EnemyState.Attack && currentState != EnemyState.Cooldown)
        {
            if (distance <= distanceToChasePlayer)
            {
                currentState = EnemyState.Chase;
            }
        }

        switch (currentState)
        {
            case EnemyState.Chase:
                if (distance <= attackRange)
                {
                    currentState = EnemyState.Attack;
                }
                else if (distance > distanceToChasePlayer)
                {
                    PickNextState(); // Player got away
                }
                else
                {
                    ChasePlayer();
                }
                break;

            case EnemyState.Attack:
                if (!hasAttacked)
                {
                    AttackPlayer();
                }
                break;

            case EnemyState.Cooldown:
                cooldownTimer -= Time.deltaTime;
                if (cooldownTimer <= 0f)
                {
                    PickNextState(); // Return to Idle or Wander
                }
                break;

            case EnemyState.Wander:
                Wander();
                break;

            case EnemyState.Idle:
                Idle();
                break;
        }
    }

    void AttackPlayer()
    {
        hasAttacked = true;

        // Damage the player if in range
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= attackRange)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }

        // After attacking, enter cooldown state
        currentState = EnemyState.Cooldown;
        cooldownTimer = attackCooldown;
        hasAttacked = false;

        Debug.Log("Enemy attacked and is cooling down.");
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SafeZone"))
        {
            // Stop Chasing and do not enter
            currentState = EnemyState.Wander;
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
        spawner?.OnEnemyDeath(gameObject);
        Destroy(gameObject); // You can replace this with a death animation/effect
    }
}
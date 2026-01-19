using UnityEngine;
using UnityEngine.AI;

public class EnemyChickDamageable : MonoBehaviour, IDamageables
{
    [Header("Chase Settings")]
    [SerializeField] private Transform _player;
    [SerializeField] private float _updateDestinationInterval = 0.2f;

    [Header("Damage Settings")]
    [SerializeField] private float _knockbackForce = 5f;
    [SerializeField] private float _damageCooldown = 2f;

    private NavMeshAgent _agent;
    private float _lastDamageTime = -Mathf.Infinity;
    private float _lastDestinationUpdateTime;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        if (_player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                _player = playerObj.transform;
            }
            else
            {
                Debug.LogWarning("EnemyChickDamageable: Player not found. Please assign the player reference or ensure Player has 'Player' tag.");
            }
        }
    }

    private void Update()
    {
        if (_player == null)
        {
            Debug.LogWarning("EnemyChickDamageable: No player reference!");
            return;
        }

        if (_agent == null)
        {
            Debug.LogWarning("EnemyChickDamageable: No NavMeshAgent component!");
            return;
        }

        if (!_agent.isOnNavMesh)
        {
            Debug.LogWarning("EnemyChickDamageable: Agent is NOT on NavMesh! Bake the NavMesh first.");
            return;
        }

        if (Time.time - _lastDestinationUpdateTime >= _updateDestinationInterval)
        {
            _agent.SetDestination(_player.position);
            _lastDestinationUpdateTime = Time.time;
        }
    }

    public void GiveDamage(Rigidbody playerRigidbody, Transform playerVisualTransform)
    {
        if (Time.time - _lastDamageTime < _damageCooldown)
            return;

        _lastDamageTime = Time.time;

        HealthManager.Instance.Damage(1);
        AudioManager.Instance.Play(SoundType.ChickSound);

        Vector3 knockbackDirection = (playerRigidbody.transform.position - transform.position).normalized;
        knockbackDirection.y = 0.3f;
        playerRigidbody.AddForce(knockbackDirection * _knockbackForce, ForceMode.Impulse);
    }
}

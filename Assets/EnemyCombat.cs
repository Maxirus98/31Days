using System.Collections;
using UnityEngine;

public class EnemyCombat : CharacterCombat
{
    [SerializeField] private float damage;
    private readonly float _detectionRadius = 6f;
    private readonly float _attackDistance = 2f;
    private EnemyAnimator _enemyAnimator;
    private GameObject _player;
    private float TimestampAttack { get; set; }
    private float AttackCooldown { get; set; } = 2f;
    private bool _hasTarget = false;
    private CharacterCombat _playerCombat;

    private void Start()
    {
        _healthbarScript.SetMaxHealth(_characterStats.maxHealth);
        _enemyAnimator = GetComponent<EnemyAnimator>();
        _player = GameObject.FindWithTag("Player");
        _playerCombat = _player.GetComponent<CharacterCombat>();
    }

    private void Update()
    {
        if (!isDead)
        {
            SetTarget();
            if (Time.time >= TimestampAttack && Vector3.SqrMagnitude(_player.transform.position - transform.position) <= _attackDistance)
            {
                StartCoroutine(AttackPlayer());
            }

            if (_hasTarget)
            {
                DirectAtPlayer();
                FollowPlayer();
            }
        }
    }

    private void DirectAtPlayer()
    {
        var direction = _player.transform.position - transform.position;
        direction.Normalize();
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 6f * Time.deltaTime);
    }

    private void FollowPlayer()
    {
        if (Vector3.SqrMagnitude(_player.transform.position - transform.position) > _attackDistance)
        {
            transform.position += transform.forward * 6f * Time.deltaTime;
            _enemyAnimator.StopAnimatingEnemy("Attack");
            _enemyAnimator.AnimateMovements("Movements", 1f);
        }
    }

    private IEnumerator AttackPlayer()
    {
        TimestampAttack = Time.time + AttackCooldown;
        _enemyAnimator.AnimateMovements("Movements", 0f);
        _enemyAnimator.AnimateEnemy("Attack");
        _playerCombat.TakeDamage(damage);
        
        yield return new WaitForSeconds(0.1f);
        _enemyAnimator.StopAnimatingEnemy("Attack");
    }

    private void SetTarget()
    {
        if(IsPlayerInRange())
        {
            _hasTarget = true;
        }
    }

    private bool IsPlayerInRange()
    {
        return Vector3.SqrMagnitude(_player.transform.position - transform.position) <= _detectionRadius;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _detectionRadius);
    }
}

using System.Collections;
using UnityEngine;

public class EnemyCombat : CharacterCombat
{
    [SerializeField] private float damage;
    private readonly float _detectionRadius = 6f;
    private readonly float _attackDistance = 2f;
    private readonly float _slerpSpeed = 6f;
    private readonly float _followSpeed = 4f;
    
    private EnemyAnimator _enemyAnimator;
    private GameObject _player;
    private float TimestampAttack { get; set; }
    private float AttackCooldown { get; set; } = 2f;
    private bool _hasTarget = false;
    private CharacterCombat _playerCombat;
    private Vector3 _initialPosition;
    private Quaternion _initialRotation;

    private void Start()
    {
        _initialPosition = transform.position;
        _initialRotation = transform.rotation;
        _healthbarScript.SetMaxHealth(_characterStats.maxHealth);
        _enemyAnimator = GetComponent<EnemyAnimator>();
        _player = GameObject.FindWithTag("Player");
        _playerCombat = _player.GetComponent<CharacterCombat>();
    }

    protected override void Update()
    {
        base.Update();
        if (!isDead && !isStunned)
        {
            SetTarget();

            if (_hasTarget)
            {
                DirectAtPlayer();
                FollowPlayer();
                if (Time.time >= TimestampAttack && Vector3.SqrMagnitude(_player.transform.position - transform.position) <= _attackDistance)
                {
                    StartCoroutine(AttackPlayer());
                }
            }
            else
            {
                ResetEnemy();
            }
        }
    }

    private float GetInitialPositionDiff()
    {
        return Vector3.SqrMagnitude(transform.position - _initialPosition);
    }

    private void ResetEnemy()
    {
        if (GetInitialPositionDiff() <= 1f) {
            _enemyAnimator.AnimateMovements("Movements", 0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, _initialRotation, _slerpSpeed * Time.deltaTime);
        }
        else
        {
            // call animation only once
            _enemyAnimator.AnimateMovements("Movements", 1f);
            var newRotation = Quaternion.LookRotation(_initialPosition);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, _slerpSpeed * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, _initialPosition, _followSpeed * Time.deltaTime);

        }
    }

    private void DirectAtPlayer()
    {
        var direction = _player.transform.position - transform.position;
        direction.Normalize();
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), _slerpSpeed * Time.deltaTime);
    }

    private void FollowPlayer()
    {
        if (Vector3.SqrMagnitude(_player.transform.position - transform.position) > _attackDistance)
        {
            transform.position += transform.forward * _followSpeed * Time.deltaTime;
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
        if(IsPlayerInRange() && !_player.layer.Equals(LayerMask.NameToLayer("Stealth")))
        {
            _hasTarget = true;
        }

        if (_player.layer.Equals(LayerMask.NameToLayer("Stealth")))
        {
            _hasTarget = false;
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

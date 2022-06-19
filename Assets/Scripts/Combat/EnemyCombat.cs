using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCombat : CharacterCombat
{
    [SerializeField] private float damage;
    private readonly float DETECTION_RADIUS = 6f;
    private readonly float ATTACK_DISTANCE = 2f;
    private readonly float SLERP_SPEED = 6f;
    
    private EnemyAnimator _enemyAnimator;
    private GameObject _player;
    private float TimestampAttack { get; set; }
    private float AttackCooldown { get; set; } = 2f;
    
    private bool _hasTarget;
    private CharacterCombat _playerCombat;
    private Vector3 _initialPosition;
    private Quaternion _initialRotation;
    
    private CombatUiScript _combatUiScript;
    private NavMeshAgent _navMeshAgent;

    protected override void Start()
    {
        base.Start();
        var enemyTransform = transform;
        _initialPosition = enemyTransform.position;
        _initialRotation = enemyTransform.rotation;
        healthbarScript.SetMaxHealth(CharacterStats.maxHealth);
        _enemyAnimator = GetComponent<EnemyAnimator>();
        _player = GameObject.FindWithTag("Player");
        _playerCombat = _player.GetComponent<CharacterCombat>();
        
        // TODO: Refactor - Child Name dependant
        _combatUiScript = AbilityUtils.FindDeepChild("DamageTextContainer", transform).GetComponent<CombatUiScript>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    protected void Update()
    {
        if (_navMeshAgent.isStopped) return;
        if (_player != null && !CurrentCharacterCombatState.Equals(CharacterCombatState.Dead))
        {
            SetTarget();
            if (_hasTarget)
            {
                FollowPlayer();
                if (Time.time >= TimestampAttack && Vector3.SqrMagnitude(_player.transform.position - transform.position) <= ATTACK_DISTANCE)
                {
                    StartCoroutine(AttackPlayer());
                }
            }
            else
            {
                ResetEnemy();
            }
        }
        else
        {
            _navMeshAgent.isStopped = true;
        }
    }
    
    private float GetInitialPositionDiff()
    {
        return Vector3.SqrMagnitude(transform.position - _initialPosition);
    }

    private void ResetEnemy()
    {
        if (GetInitialPositionDiff() <= 1f && CurrentCharacterCombatState.Equals(CharacterCombatState.OutCombat)) {
            _enemyAnimator.AnimateMovements("Movements", 0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, _initialRotation, SLERP_SPEED * Time.deltaTime);
        }
        else
        {
            _enemyAnimator.AnimateMovements("Movements", CharacterStats.movementSpeed);
            var newRotation = Quaternion.LookRotation(_initialPosition);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, SLERP_SPEED * Time.deltaTime);
            _navMeshAgent.destination = _initialPosition;
        }
    }

    private void FollowPlayer()
    {
        if (Vector3.SqrMagnitude(_player.transform.position - transform.position) > ATTACK_DISTANCE)
        {
            _navMeshAgent.destination = _player.transform.position;
            _enemyAnimator.StopAnimatingEnemy("Attack");
            _enemyAnimator.AnimateMovements("Movements", CharacterStats.movementSpeed);
        }
    }

    public override float TakeDamage(float pDamage)
    {
        var damageDone = base.TakeDamage(pDamage);
        _combatUiScript.PrintDamage(damageDone);
        return damageDone;
    }
    
    public override float TakeDamageNoAnimation(float pDamage)
    {
        var damageDone = base.TakeDamage(pDamage);
        _combatUiScript.PrintDamage(damageDone);
        return damageDone;
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
        if(IsPlayerInRange() || CurrentCharacterCombatState.Equals(CharacterCombatState.InCombat))
        {
            _hasTarget = true;
        }

        if (_player.layer.Equals(LayerMask.NameToLayer("Stealth")) || _playerCombat.CurrentCharacterCombatState == CharacterCombatState.Dead)
        {
            _hasTarget = false;
        }
    }

    private bool IsPlayerInRange()
    {
        return _player != null &&  Vector3.SqrMagnitude(_player.transform.position - transform.position) <= DETECTION_RADIUS;
    }
}

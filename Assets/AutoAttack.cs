using System.Collections;
using UnityEngine;

public class AutoAttack : AutoTargetSpell
{
    public static bool Blocked { get; set; }
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private LayerMask enemyLayers;
    public void Awake()
    {
        Cooldown = 2f;
        Name = "AutoAttacking";
        Range = 15f;
        Blocked = false;
    }

    private void Update()
    {
        if (Time.time > Timestamp)
        {
            StartCoroutine(nameof(DoSpell));
        }
    }

    protected override IEnumerator DoSpell()
    {
        if (IsInRange(Range))
        {
            Timestamp = Time.time + Cooldown;
            DamageEnemiesHit();
            _playerAnimator.AnimateSpell(Name);
            yield return new WaitForSeconds(0.1f);
            _playerAnimator.StopAnimatingSpell(Name);

        }
    }
    
    void DamageEnemiesHit()
    {
        //Detect enemies in range of attack
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider enemy in hitEnemies)
        {
            Debug.Log("We hit " + enemy.name);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}

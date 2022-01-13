using System.Collections;
using UnityEngine;

public class AutoAttack : AutoTargetSpell
{
    public static bool Blocked { get; set; }
    public void Awake()
    {
        Cooldown = 2f;
        Name = "AutoAttacking";
        BaseDamage = 100f;
        Range = 15f;
        Blocked = false;
    }

    private void Update()
    {
        if (Time.time > Timestamp)
        {
            DamageEnemiesHit();
            StartCoroutine(nameof(DoSpell));
        }
    }

    protected override IEnumerator DoSpell()
    {
        if (IsInRange(Range))
        {
            Timestamp = Time.time + Cooldown;
            _playerAnimator.AnimateSpell(Name);
            yield return new WaitForSeconds(0.1f);
            _playerAnimator.StopAnimatingSpell(Name);

        }
    }
}

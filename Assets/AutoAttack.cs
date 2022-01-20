using System.Collections;
using UnityEngine;

public class AutoAttack : AutoTargetSpell
{
    public static bool Blocked { get; set; }
    public void Awake()
    {
        if (cooldown <= 0) cooldown = 2f;
        Name = "AutoAttacking";
        BaseDamage = 100f;
        Range = 15f;
        Blocked = false;
    }

    private void Update()
    {
        base.Update();
        if (Time.time > Timestamp)
        {
            StartCoroutine(nameof(DoSpell));
        }
    }

    protected override IEnumerator DoSpell()
    {
        if (IsInRange(Range))
        {
            Timestamp = Time.time + cooldown;
            CharacterCombat.onStateChangeHandler.Invoke(CharacterState.InCombat);
            var direction = AbilityUtils.DirectAt(transform, MouseManager.focus);
            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
            cloneDmgSender = Instantiate(damageSender, transform.position, Quaternion.identity);
            DamageDid = false;
            _playerAnimator.AnimateSpell(Name);
            yield return new WaitForSeconds(0.1f);
            _playerAnimator.StopAnimatingSpell(Name);
            attackCallback(CharacterState.InCombat);
        }
    }
}

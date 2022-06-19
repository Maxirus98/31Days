using System.Collections;
using UnityEngine;

public class AutoAttack : AutoTargetSpell
{
    public delegate void SpreadAttack();
    public SpreadAttack spreadAttack;
    
    public void Awake()
    {
        if (cooldown <= 0) cooldown = 2f;
        Name = "AutoAttacking";
        BaseDamage = 100f;
        Range = 15f;
        spellSlot = 5;
    }

    private new void Update()
    {
        base.Update();
        if (Time.time > Timestamp)
        {
            StartCoroutine(nameof(DoSpell));
        }
    }

    protected override IEnumerator DoSpell()
    {
        if (IsEnemyInRange(Range) && IsLookingAt(MouseManager.focus.transform))
        {
            Timestamp = Time.time + cooldown;
            CharacterCombat.onCombatStateChangeHandler.Invoke(CharacterCombatState.InCombat);
            cloneDmgSender = Instantiate(damageSender, transform.position, Quaternion.identity);
            DamageDid = false;
            playerAnimator.AnimateSpell(Name);
            yield return new WaitForSeconds(0.1f);
            playerAnimator.StopAnimatingSpell(Name);
            attackCallback(CharacterCombatState.InCombat);
            
            // Warrior only - to refactor
            spreadAttack?.Invoke();
        }
    }
}

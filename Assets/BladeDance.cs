using System.Collections;
using UnityEngine;

public class BladeDance : AutoTargetSpell
{
    private void Awake()
    {
        Name = "Blade Dance";
        Description = "2 consecutive attacks. Blade Dance is more effective while stealth.";
        Cooldown = 1f;
        Range = 15f;
        IsAutoTarget = false;
    }

    private void Update()
    {
        if (Time.time >= Timestamp && Input.GetKeyDown(KeyCode.Alpha1) && IsInRange(Range))
        {
            StartCoroutine(nameof(DoSpell));
        }
    }

    protected override IEnumerator DoSpell()
    {
        if (MouseManager && MouseManager.focus)
        {
            StartCoroutine(nameof(AnimatePlayer));
            Timestamp = Time.time + Cooldown;
        }
        
        yield return new WaitForSeconds(1f);    
    }


    protected override IEnumerator AnimatePlayer()
    {
        AutoAttack.Blocked = true;
        _playerAnimator.AnimateSpell(Name);
        yield return new WaitForSeconds(0.1f);
        _playerAnimator.StopAnimatingSpell(Name);
        AutoAttack.Blocked = false;
    }
}
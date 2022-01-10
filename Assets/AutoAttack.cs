using System.Collections;
using UnityEngine;

public class AutoAttack : AutoTargetSpell
{
    public static bool Blocked { get; set; }
    public void Awake()
    {
        Cooldown = 2f;
        Name = "AutoAttacking";
        Range = 15f;
        Blocked = false;
    }

    private void Update()
    {
        if (Blocked)
        {
            _playerAnimator.StopAnimatingSpell(Name);
        }
        if (Time.time > Timestamp)
        {
            StartCoroutine(nameof(DoSpell));
            Timestamp = Time.time + Cooldown;
        }
    }

    protected override IEnumerator DoSpell()
    {
        if (IsInRange(Range))
        {
            Timestamp = Time.time + Cooldown;
            _playerAnimator.AnimateSpell(Name);

            yield break;
        }
        _playerAnimator.StopAnimatingSpell(Name);

    }
}

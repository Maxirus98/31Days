﻿using System;
using System.Collections;
using UnityEngine;

public class AutoAttack : AutoTargetSpell
{
    public static bool Blocked { get; set; }
    void Awake()
    {
        Cooldown = 0.5f;
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
        if (!Blocked && Time.time >= Timestamp)
        {
            StartCoroutine(nameof(DoSpell));
            Timestamp = Time.time + Cooldown;
        }
    }

    protected override IEnumerator DoSpell()
    {
        StartCoroutine(nameof(AnimatePlayer));
        yield return new WaitForSeconds(0.1f);
    }

    protected override IEnumerator AnimatePlayer()
    {
        if (IsInRange(Range))
        {
            _playerAnimator.AnimateSpell(Name);
            Timestamp = Time.time + Cooldown;
            yield return new WaitForSeconds(0.1f);
        }
        else
        {
            _playerAnimator.StopAnimatingSpell(Name);
        }
    }
}

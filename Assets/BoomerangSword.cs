using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = System.Numerics.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class BoomerangSword : ThrowSpells
{
    private void Awake()
    {
        Name = "Boomerang Sword";
        TravelSpeed = 6f;
        Description = "You throw your sword in front of you and it comes back at you dealing damage and slowing enemies in its path";
        Cooldown = 1f;
        BaseDamage = 100;
        IsAutoTarget = false;
    }
    
    private void Update()
    {
        if (Time.time > Timestamp && Input.GetKeyDown(KeyCode.Alpha1))
        {
            StartCoroutine(nameof(DoSpell));
        }
    }

    protected override IEnumerator DoSpell()
    {
        Timestamp = Time.time + Cooldown;
        var playerTransform = transform;
        var playerForward = playerTransform.forward;

        StartCoroutine(nameof(AnimatePlayer));
        
        var spell = Instantiate(_initialSummon, playerTransform.position, playerTransform.rotation);
        var rbSpell = spell.gameObject.GetComponent<Rigidbody>();
        
        spell.transform.Translate(Vector3.forward + Vector3.up);
        spell.transform.Rotate(Vector3.right * 90);
        rbSpell.velocity =  playerForward * 10;
        yield return new WaitForSeconds(1f);
        rbSpell.velocity =  playerForward * -10;
        Destroy(spell.gameObject, 1f);
    }

    protected override IEnumerator AnimatePlayer()
    {
        Debug.Log("animate player with spellName" + Name);
        _playerAnimator.AnimateSpell(Name);
        yield return new WaitForSeconds(0.1f);
        _playerAnimator.StopAnimatingSpell(Name);
    }
}

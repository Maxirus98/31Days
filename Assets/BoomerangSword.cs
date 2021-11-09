using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
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
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            StartCoroutine(nameof(DoSpell));
        }
    }

    protected override IEnumerator DoSpell()
    {
        var playerTransform = transform;
        var playerForward = playerTransform.forward;
        
        AnimatePlayer();
        
        var spell = Instantiate(_initialSummon, playerTransform.position, playerTransform.rotation);
        var rbSpell = spell.gameObject.GetComponent<Rigidbody>();
        
        spell.transform.Translate(Vector3.forward + Vector3.up);
        spell.transform.Rotate(Vector3.right * 90);
        rbSpell.velocity =  playerForward * 10;
        yield return new WaitForSeconds(1);
        
        rbSpell.velocity =  playerForward * -10;
        Destroy(spell.gameObject, 1);
    }

    private void AnimatePlayer()
    {
        _playerAnimator.AnimateSpell(name);
    }
}

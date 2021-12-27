using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charge : Spells
{
    private Collider _collider;
    private Rigidbody _rigidbody;
    private ParticleSystem _chargeEffect;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _chargeEffect = transform.Find("ChargeEffect").GetComponent<ParticleSystem>();
        Name = "Charge";
        Description = "You charge forward hitting and stunning the enemy";
        Cooldown = 1f;
        BaseDamage = 10;
        IsAutoTarget = true;
    }

    private void Update()
    {
        if (Time.time > Timestamp && Input.GetKeyDown(KeyCode.Alpha3))
        {
            StartCoroutine(nameof(DoSpell));
        }
    }

    protected override IEnumerator DoSpell()
    {
        Timestamp = Time.time + Cooldown;
        if(!_chargeEffect.isPlaying)_chargeEffect.Play(false);
        var constraints = _rigidbody.constraints;
        _rigidbody.constraints = constraints | RigidbodyConstraints.FreezePositionY;
        _rigidbody.detectCollisions = false;
        StartCoroutine(nameof(AnimatePlayer));
        yield return new WaitForSeconds(0.5f);
        if(_chargeEffect.isPlaying)_chargeEffect.Stop(false);
        _rigidbody.detectCollisions = true;
        _rigidbody.constraints = constraints;


    }
    
    protected override IEnumerator AnimatePlayer()
    {
        Debug.Log("animate player with spellName" + Name);
        _playerAnimator.AnimateSpell(Name);
        yield return new WaitForSeconds(0.1f);
        _playerAnimator.StopAnimatingSpell(Name);
    }
}

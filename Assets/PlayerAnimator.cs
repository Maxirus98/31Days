﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Jump))]
public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    private Jump _jump;
    private AutoAttack _autoAttack;
    private MouseManager _mouseManager;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _jump = GetComponent<Jump>();
        _autoAttack = GetComponent<AutoAttack>();
        
        // TODO : Refactor to not have to wait to get the MouseManager after level is loaded
        StartCoroutine(nameof(GetMouseManager));
    }

    IEnumerator GetMouseManager()
    {
        yield return new WaitForSeconds(1);
        _mouseManager = GameObject.Find("MouseManager").GetComponent<MouseManager>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveAnimation();
        StartAutoAttackAnimation();
        JumpAnimation();
    }
    
    void MoveAnimation()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        
        _animator.SetFloat("VelocityX", horizontal);
        _animator.SetFloat("VelocityZ", vertical);
    }

    private void JumpAnimation()
    {
        if (Time.time > _jump.Timestamp)
        {
            float jumpAxis = Input.GetAxis("Jump");
            _animator.SetFloat("Jump", jumpAxis);
            _jump.Timestamp = Time.time + _jump.Cooldown;
            return;
        }
        _animator.SetFloat("Jump", 0);
    }
    
    private void StartAutoAttackAnimation()
    {
        if (Time.time > _autoAttack.Timestamp)
        {
            if (_mouseManager && _mouseManager.focus)
            {
                _animator.SetBool("AutoAttacking", isInMeleeRange());
                _autoAttack.Timestamp = Time.time + _autoAttack.Cooldown;
            }
        }
    }

    private bool isInMeleeRange()
    {
        if(_mouseManager.focus)
            return Vector3.SqrMagnitude(transform.position - _mouseManager.focus.transform.position) < _autoAttack.range;
        return false;
    }

    public void AnimateSpell(string spellName)
    {
        _animator.SetBool(spellName, true);
    }

    public void StopAnimatingSpell(string spellName)
    {
        _animator.SetBool(spellName, false);
    }
}

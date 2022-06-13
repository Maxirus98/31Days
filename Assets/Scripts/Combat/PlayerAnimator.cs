using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Jump))]
public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    private Jump _jump;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _jump = GetComponent<Jump>();
    }

    void Update()
    {
        MoveAnimation();
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

    public void AnimateSpell(string spellName)
    {
        _animator.SetBool(spellName, true);
    }

    public void StopAnimatingSpell(string spellName)
    {
        _animator.SetBool(spellName, false);
    }
}

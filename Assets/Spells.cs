using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spells : MonoBehaviour
{
    [SerializeField] protected string Name { get; set; }
    [SerializeField] protected string Description { get; set; }
    public float Cooldown { get; set; }
    public float Timestamp { get; set; }

    [SerializeField] protected float BaseDamage { get; set; }
    [SerializeField] protected bool IsAutoTarget { get; set; }

    protected PlayerAnimator _playerAnimator;
    protected Animator _animator;
    
    protected virtual void Start()
    {
        _playerAnimator = GetComponent<PlayerAnimator>();
        _animator = GetComponent<Animator>();
    }

    protected virtual IEnumerator DoSpell()
    {
        throw new NotImplementedException();
    }

    protected virtual IEnumerator AnimatePlayer()
    {
        throw new NotImplementedException();
    }
}

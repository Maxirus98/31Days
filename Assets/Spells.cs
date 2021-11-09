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
    
    private void Awake()
    {
        _playerAnimator = GetComponent<PlayerAnimator>();
    }

    protected virtual IEnumerator DoSpell()
    {
        throw new NotImplementedException();
    }

    protected virtual void AnimatePlayer()
    {
        throw new NotImplementedException();
    }
}

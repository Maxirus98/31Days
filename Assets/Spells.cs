using System;
using System.Collections;
using UnityEngine;

public class Spells : MonoBehaviour
{
    protected delegate void AttackCallback(CharacterState characterState);
    protected CharacterCombat CharacterCombat;
    protected AttackCallback attackCallback;
    protected string Name { get; set; }
    protected string Description { get; set; }
    public float cooldown;
    public float Timestamp { get; set; }

    public float BaseDamage { get; set; }
    protected bool IsAutoTarget { get; set; }
    protected float Range { get; set; }
    public float hitRadius = 1f;

    [SerializeField] protected Transform attackPoint;
    [SerializeField] protected LayerMask enemyLayers;
    [SerializeField] protected PlayerAnimator _playerAnimator;
    protected Animator animator;
    
    protected virtual void Start()
    {
        CharacterCombat = GetComponent<CharacterCombat>();
        attackCallback = CharacterCombat.UpdateCharacterState;
        _playerAnimator = GetComponent<PlayerAnimator>();
        animator = GetComponent<Animator>();
    }

    protected virtual IEnumerator DoSpell()
    {
        throw new NotImplementedException();
    }

    protected virtual IEnumerator AnimatePlayer()
    {
        throw new NotImplementedException();
    }

    protected void StopCurrentAnimation(string name)
    {
        _playerAnimator.StopAnimatingSpell(name);
    }
}

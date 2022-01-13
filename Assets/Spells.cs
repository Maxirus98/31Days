using System;
using System.Collections;
using UnityEngine;

public class Spells : MonoBehaviour
{
    protected string Name { get; set; }
    protected string Description { get; set; }
    public float Cooldown { get; set; }
    public float Timestamp { get; set; }

    protected float BaseDamage { get; set; }
    protected bool IsAutoTarget { get; set; }
    protected float Range { get; set; }
    public float _swordRadius = 1f;

    [SerializeField] protected Transform attackPoint;
    [SerializeField] protected LayerMask enemyLayers;
    [SerializeField] protected PlayerAnimator _playerAnimator;
    protected Animator _animator;
    
    protected void Start()
    {
        _playerAnimator = GetComponent<PlayerAnimator>();
        _animator = GetComponent<Animator>();
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, _swordRadius);
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

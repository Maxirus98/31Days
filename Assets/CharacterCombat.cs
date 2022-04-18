using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public enum CharacterCombatState
{
    InCombat,
    OutCombat,
    Dead,
}

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    [Serializable]
    public class CharacterCombatStateEvent : UnityEvent<CharacterCombatState> {}
    
    public HealthbarScript healthbarScript;
    public CharacterCombatStateEvent onCombatStateChangeHandler;
    public CharacterCombatState CurrentCharacterCombatState {get; set;}
    
    protected CharacterStats CharacterStats;
    protected delegate void OnDeath();
    protected OnDeath OnDeathCallback;
    
    private Animator _animator;
    private Rigidbody _rigidbody;
    private Collider _collider;
    private ParticleSystem _stunEffect;
    private readonly float _takeDamageCooldown = 2f;
    
    private void Awake()
    {
        CharacterStats = GetComponent<CharacterStats>();
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        OnDeathCallback = Die;
        CurrentCharacterCombatState = CharacterCombatState.OutCombat;
    }

    protected virtual void Start()
    {
        if(healthbarScript == null)
            Debug.LogWarning("Healthbar Script is not set for " + gameObject.name);
    }

    public void UpdateCharacterCombatState(CharacterCombatState characterCombatState)
    {
        CurrentCharacterCombatState = characterCombatState;
        onCombatStateChangeHandler.Invoke(characterCombatState);
    }
    
    public virtual float TakeDamage(float damage)
    {
        var damageDone = damage / CharacterStats.defense;
        CharacterStats.health -= damageDone;
        if (CharacterStats.health > 0)
        {
            _animator.SetTrigger("TakeDamage");
            Debug.Log($"{gameObject.name} took {damageDone} damage.");
            UpdateCharacterCombatState(CharacterCombatState.InCombat);
        }
        
        healthbarScript.SetHealth(CharacterStats.health);
        if (CharacterStats.health <= 0)
        {
            OnDeathCallback.Invoke();
        }

        return damageDone;
    }
    
    public void TakeDamageNoAnimation(float damage)
    {
        var damageDone = damage / CharacterStats.defense;
        CharacterStats.health -= damageDone;
        if (CharacterStats.health > 0)
        {
            UpdateCharacterCombatState(CharacterCombatState.InCombat);
        }
        healthbarScript.SetHealth(CharacterStats.health);
        if (CharacterStats.health <= 0)
        {
            OnDeathCallback.Invoke();
        }
    }

    protected virtual void Die()
    {
        DeactivateEnemy();
        UpdateCharacterCombatState(CharacterCombatState.Dead);
        _animator.SetTrigger("Die");
        Destroy(gameObject, 5f);
    }
    
    public void DeactivateComponents()
    {
        foreach (var behaviour in GetComponents<Behaviour>())
        {
            behaviour.enabled = false;
        }
    }

    // Move to another class ?
    public IEnumerator Stun(float duration)
    {
        // prevent from attacking and moving
        // Playing stun animation
        
        if(!_stunEffect.isPlaying)_stunEffect.Play();
        yield return new WaitForSeconds(duration);
        if(_stunEffect.isPlaying)_stunEffect.Stop();
    }

    private void DeactivateEnemy()
    {
        _rigidbody.useGravity = false;
        _collider.enabled = false;
    }
}

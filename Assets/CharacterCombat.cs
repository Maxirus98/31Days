using System;
using UnityEngine;
using UnityEngine.Events;

public enum CharacterState
{
    InCombat,
    OutCombat
}



[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    [Serializable]
    public class CharacterStateEvent : UnityEvent<CharacterState>
    {
        //1. déFINIR LA CLASSE D'ÉVÉNEMENT avec les param qu'on veut passer
    }
    
    private CharacterStats _characterStats;
    private Animator _animator;
    private MouseManager _mouseManager;
    
    protected Rigidbody _rigidbody;
    protected Collider _collider;
    
    public HealthbarScript _healthbarScript;
    
    public CharacterStateEvent onStateChangeHandler;
    public CharacterState CurrentCharacterState {get; set;}
    
    private void Awake()
    {
        _characterStats = GetComponent<CharacterStats>();
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _mouseManager = GameObject.Find("MouseManager").GetComponent<MouseManager>();
    }

    private void Start()
    {
        _healthbarScript.SetMaxHealth(_characterStats.maxHealth);
    }

    public void UpdateCharacterState(CharacterState characterState)
    {
        CurrentCharacterState = characterState;
        onStateChangeHandler.Invoke(characterState);
    }

    public void TakeDamage(float damage)
    {
        float damageDone  = damage / _characterStats.defense;
        Debug.Log("took " + damageDone);
        _characterStats.health -= damageDone;
        if (_characterStats.health > 0)
        {
            _animator.SetTrigger("TakeDamage");
        }
        _healthbarScript.SetHealth(_characterStats.health);
        if (_characterStats.health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        DesactivateEnemy();
        _animator.SetTrigger("Die");
        Destroy(gameObject, 5f);
    }

    private void DesactivateEnemy()
    {
        _rigidbody.useGravity = false;
        _collider.enabled = false;
    }
}

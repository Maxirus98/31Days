using System;
using System.Collections;
using System.Security.Cryptography;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    private CharacterStats _characterStats;
    private Animator _animator;
    private MouseManager _mouseManager;
    public HealthbarScript _healthbarScript;

    private void Awake()
    {
        _characterStats = GetComponent<CharacterStats>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _healthbarScript.setMaxHealth(_characterStats.maxHealth);
    }

    public void TakeDamage(float damage)
    {
        float damageDone  = damage / _characterStats.defense;
        _characterStats.health -= damageDone;
        _animator.SetTrigger("TakeDamage");
        _healthbarScript.SetHealth(_characterStats.health);
        Debug.Log(gameObject.name + "hit for " + damageDone + " damage");
        Die();
    }

    private void Die()
    {
        if (_characterStats.health <= 0)
        {
            _mouseManager = GameObject.Find("MouseManager").GetComponent<MouseManager>();
            _animator.SetTrigger("Die");
            _mouseManager.SetFocus(null);
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Collider>().enabled = false;
            Debug.Log(gameObject.name + " just died");
            Destroy(gameObject, 5f);
        }
    }
}

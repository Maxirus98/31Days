using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CleaveScript : MonoBehaviour
{
    private readonly float HitRadius = 5f;
    [SerializeField] private LayerMask enemyLayers;
    private GameObject _player;
    private Cleave _cleave;
    public List<Collider> _collidersCollidedWith = new List<Collider>();
    private MeshRenderer _sword;
    private AutoAttack _autoAttack;
    private bool _hasCollided = false;
    private MouseManager _mouseManager;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _cleave = _player.GetComponent<Cleave>();
        _autoAttack = _player.GetComponent<AutoAttack>();
        _mouseManager = GameObject.Find("MouseManager").GetComponent<MouseManager>();
    }

    private void OnTriggerEnter (Collider other)
    {
        if (!other.CompareTag("Enemy")) return;
        FillCollidersCollidedWith(other);
    }

    private void OnDestroy()
    {
        DamageMultipleEnemies(_cleave.BaseDamage);
        if (_collidersCollidedWith.Count > 0)
        {
            _autoAttack.spreadAttack = AutoAttackMultipleEnemies;
        }
    }

    private void DamageMultipleEnemies(float damage)
    {
        foreach (var enemy in _collidersCollidedWith.Select(enemyCollider => enemyCollider.gameObject.GetComponent<CharacterCombat>()))
        {
            enemy.TakeDamage(damage);
        }
    }
        
    private void AutoAttackMultipleEnemies()
    {
        foreach (var enemy in _collidersCollidedWith.Select(enemyCollider => enemyCollider.gameObject.GetComponent<CharacterCombat>()))
        {
            if(_mouseManager.focus.gameObject != enemy.gameObject)
                enemy.TakeDamage(_autoAttack.BaseDamage);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, HitRadius);
    }
    
    private void FillCollidersCollidedWith(Collider enemy)
    {
        _collidersCollidedWith.Add(enemy);
    }
}

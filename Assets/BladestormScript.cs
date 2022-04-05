using System;
using System.Collections.Generic;
using UnityEngine;

public class BladestormScript : MonoBehaviour
{
    private readonly string ENEMY_TAG = "Enemy";
    private readonly float _repeatRate = 0.8f;
    
    [SerializeField]private List<Collider> _collidersCollidedWith = new List<Collider>();
    private BoomerangSword _boomerangSword;
    private float Timestamp { get; set; }
    

    private void Start()
    {
        _boomerangSword = GetComponentInParent<BoomerangSword>();
    }

    private void Update()
    {
        if (Time.time > Timestamp)
        {
            DamageEnemies();
        }
    }
    
    private void DamageEnemies()
    {
        Timestamp = Time.time + _repeatRate;
        foreach (var collider in  _collidersCollidedWith)
        {
            if (collider == null)
            {
                _collidersCollidedWith.Remove(collider);
            }
            var enemy = collider.GetComponent<CharacterCombat>();
            enemy.TakeDamage(_boomerangSword.BaseDamage);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(ENEMY_TAG)) return;
        if(!_collidersCollidedWith.Contains(other))
            _collidersCollidedWith.Add(other);
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;

public class BladestormScript : MonoBehaviour
{
    [SerializeField]private List<Collider> _collidersCollidedWith = new List<Collider>();

    private readonly string ENEMY_TAG = "Enemy";
    private readonly float REPEAT_RATE = 0.5f;
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
        Timestamp = Time.time + REPEAT_RATE;
        foreach (var enemy in  _collidersCollidedWith)
        {
            if (enemy == null)
            {
                _collidersCollidedWith.Remove(enemy);
            }
            var enemyCombat = enemy.GetComponent<CharacterCombat>();
            enemyCombat.TakeDamage(_boomerangSword.BaseDamage);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(ENEMY_TAG)) return;
        if(!_collidersCollidedWith.Contains(other))
            _collidersCollidedWith.Add(other);
    }
}

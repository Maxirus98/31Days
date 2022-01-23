using System;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangSwordScript : MonoBehaviour
{
    private readonly float HitRadius = 1f;
    [SerializeField] private LayerMask enemyLayers;
    private BoomerangSword _boomerangSword;
    private List<Collider> _collidersCollidedWith = new List<Collider>();

    void Start()
    {
        _boomerangSword = transform.parent.gameObject.GetComponent<BoomerangSword>();
        _boomerangSword.boomerangSwordScript = this;
    }

    // Update is called once per frame
    void Update()
    {
        DamageEnemiesHit();
    }

    private void DamageEnemiesHit()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, HitRadius, enemyLayers);
        foreach (Collider enemy in hitEnemies)
        {
            if (_collidersCollidedWith.Count == 0)
            {
                var enemyCombat = enemy.GetComponent<CharacterCombat>();
                FillCollidersCollidedWith(enemy);
                enemyCombat.TakeDamage(_boomerangSword.BaseDamage);
            }
            else
            {
                foreach (Collider collided in _collidersCollidedWith)
                {
                    if (!enemy.Equals(collided))
                    {
                        var enemyCombat = enemy.GetComponent<CharacterCombat>();
                        FillCollidersCollidedWith(enemy);
                        enemyCombat.TakeDamage(_boomerangSword.BaseDamage);
                    }
                }
            }
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

    public void ClearCollidersCollidedWith()
    {
        _collidersCollidedWith.Clear();
    }
}

using System;
using UnityEngine;

public class EnemyCombat : CharacterCombat
{
    private readonly float _detectionRadius = 6f;
    private GameObject _player;
    
    private void Attack()
    {
        
    }

    private void DetectPlayerInRadius()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _detectionRadius);
    }
}

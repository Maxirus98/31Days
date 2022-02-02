using System;
using System.Collections.Generic;
using UnityEngine;

public class ChannelingFireScript : MonoBehaviour
{
    public List<Collider> _collidersCollidedWith = new List<Collider>();

    private GameObject _player;
    private ChannelingFire _channelingFire;
    private float Timestamp { get; set; }

    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _channelingFire = _player.GetComponent<ChannelingFire>();
        Timestamp = _channelingFire.DAMAGE_INTERVAL;
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
        Timestamp = Time.time + _channelingFire.DAMAGE_INTERVAL;
        foreach (var collider in  _collidersCollidedWith)
        {
            var enemy = collider.GetComponent<CharacterCombat>();
            enemy.TakeDamageNoAnimation(_channelingFire.BaseDamage);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        FillCollidersCollidedWith(other);
    }
    
    private void FillCollidersCollidedWith(Collider enemy)
    {
        if(!_collidersCollidedWith.Contains(enemy))
            _collidersCollidedWith.Add(enemy);
    }
}

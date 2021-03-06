using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeBomb : Spells
{
    [SerializeField] private GameObject smokeBomb;
    private BladeDance _bladeDance;
    private GameObject _cloneSmokeBomb;
    private float Duration { get; set; }
    private readonly float RADIUS = 6f;
    private void Awake()
    {
        Name = "Smoke Bomb";
        Description = "While inside Smoke Bomb, Blade Dance's cooldown is reduced and deals bonus damage.";
        cooldown = 1f;
        Duration = 8f;
        _bladeDance = GetComponent<BladeDance>();
    }

    private void Update()
    {
        if (Time.time > Timestamp && Input.GetKeyDown(KeyCode.Alpha4))
        {
            StartCoroutine(nameof(DoSpell));
        }

        if (_cloneSmokeBomb)
        {
            BuffBladeDance();
            
            if (Vector3.SqrMagnitude(transform.position - _cloneSmokeBomb.transform.position) >= RADIUS && _bladeDance.isBuffed)
            {
                DebuffBladeDance();
            }
        }

        
    }

    protected override IEnumerator DoSpell()
    {
        _cloneSmokeBomb = Instantiate(smokeBomb, transform.position, transform.rotation);
        yield return new WaitForSeconds(Duration);
        Destroy(_cloneSmokeBomb);
        DebuffBladeDance();
    }

    private void BuffBladeDance()
    {
        if (Vector3.SqrMagnitude(transform.position - _cloneSmokeBomb.transform.position) < RADIUS && !_bladeDance.isBuffed)
        {
            Debug.Log("buff blade dance in range");
            _bladeDance.BuffDamage();
            _bladeDance.isBuffed = true;
            _bladeDance.cooldown = 1f;
        }
    }

    private void DebuffBladeDance()
    {
            _bladeDance.DebuffDamage();
            _bladeDance.isBuffed = false;
            _bladeDance.cooldown = _bladeDance.initialCooldown;
        
    }
}

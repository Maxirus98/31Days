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
        Name = "Vanish";
        Description = "The Rogue hides in the shadows.";
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
            ToggleBladeDanceDamage();
        }
    }

    protected override IEnumerator DoSpell()
    {
        _cloneSmokeBomb = Instantiate(smokeBomb, transform.position, transform.rotation);
        yield return new WaitForSeconds(Duration);
        Destroy(_cloneSmokeBomb);
    }

    private void ToggleBladeDanceDamage()
    {
        if (Vector3.SqrMagnitude(transform.position - _cloneSmokeBomb.transform.position) < RADIUS && !_bladeDance.isBuffed)
        {
            _bladeDance.BuffDamage();
        }
        else if(Vector3.SqrMagnitude(transform.position - _cloneSmokeBomb.transform.position) >= RADIUS && _bladeDance.isBuffed)
        {
           _bladeDance.DebuffDamage();
        }
    }
}

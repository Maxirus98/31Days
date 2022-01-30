using System;
using System.Collections;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class BoomerangSword : ThrowSpells
{
    public GameObject indicator;
    private GameObject _cloneIndicator;
    private GameObject _cloneSword;
    private Transform _playerTransform;
    private Vector3 _targetPos;
    private Vector3 _initPos;
    
    [NonSerialized]
    public BoomerangSwordScript boomerangSwordScript;
    private void Awake()
    {
        Name = "Boomerang Sword";
        TravelSpeed = 6f;
        Description = "You throw your sword in front of you and it comes back at you dealing damage and slowing enemies in its path";
        cooldown = 1f;
        TravelTime = 1f;
        BaseDamage = 100;
        IsAutoTarget = false;
        _playerTransform = transform;
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && !_cloneIndicator)
        {
            SpawnIndicator();
        }

        if (Time.time > Timestamp && _cloneIndicator && Input.GetMouseButton(0))
        {
            StartCoroutine(nameof(DoSpell));
            Destroy(_cloneIndicator);
        }

        if (_cloneIndicator && Input.GetMouseButton(1))
        {
            Destroy(_cloneIndicator);
        }
    }
    

    private void SpawnIndicator()
    {
        Vector3 playerPos = _playerTransform.position;
        Vector3 playerDirection = _playerTransform.forward;
        Quaternion playerRotation = _playerTransform.rotation;
        Vector3 spawnPos = playerPos + playerDirection * indicator.transform.localScale.z * _playerTransform.localScale.z;
        
        _cloneIndicator = Instantiate(indicator, spawnPos + Vector3.up * 0.1f, playerRotation, _playerTransform);
    }

    protected override IEnumerator DoSpell()
    {
        Timestamp = Time.time + cooldown;
        var playerTransform = transform;

        StartCoroutine(nameof(AnimatePlayer));

        _cloneSword = Instantiate(_initialSummon, _playerTransform.position, playerTransform.rotation);
        _cloneSword.transform.Rotate(Vector3.right * 90);

        yield return new WaitForSeconds(TravelTime);
        
        boomerangSwordScript.ClearCollidersCollidedWith();
    }

    protected override IEnumerator AnimatePlayer()
    {
        Debug.Log("animate player with spellName" + Name);
        _playerAnimator.AnimateSpell(Name);
        yield return new WaitForSeconds(0.1f);
        _playerAnimator.StopAnimatingSpell(Name);
    }
}

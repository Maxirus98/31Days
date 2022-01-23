using System;
using System.Collections;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class BoomerangSword : ThrowSpells
{
    [SerializeField] private GameObject indicator;
    private GameObject _cloneIndicator;
    
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
    }

    private void SpawnIndicator()
    {
        Vector3 playerPos = transform.position;
        Vector3 playerDirection = transform.forward;
        Quaternion playerRotation = transform.rotation;
        Vector3 spawnPos = playerPos + playerDirection*indicator.transform.localScale.z;
        
        _cloneIndicator = Instantiate(indicator, spawnPos + Vector3.up * 0.1f, playerRotation, transform);
    }

    protected override IEnumerator DoSpell()
    {
        Timestamp = Time.time + cooldown;
        var playerTransform = transform;
        var playerForward = playerTransform.forward;

        StartCoroutine(nameof(AnimatePlayer));
        
        var cloneSword = Instantiate(_initialSummon, playerTransform.position, playerTransform.rotation, playerTransform);
        var rb = cloneSword.GetComponent<Rigidbody>();
        rb.velocity = playerForward * TravelSpeed;
        cloneSword.transform.Translate(Vector3.forward + Vector3.up);
        cloneSword.transform.Rotate(Vector3.right * 90);
        yield return new WaitForSeconds(TravelTime);
        rb.velocity = playerForward * TravelSpeed * -1f;
        boomerangSwordScript.ClearCollidersCollidedWith();
        Destroy(cloneSword, TravelTime);
    }

    protected override IEnumerator AnimatePlayer()
    {
        Debug.Log("animate player with spellName" + Name);
        _playerAnimator.AnimateSpell(Name);
        yield return new WaitForSeconds(0.1f);
        _playerAnimator.StopAnimatingSpell(Name);
    }
}

using System.Collections;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Tsunami : ThrowSpells
{
    private void Awake()
    {
        Name = "Typhoon";
        Description = "Summon a tsunami in front of you pushing back enemies. (80 mana)";
        Cooldown = 1f;
        Range = 1.2f;
        TravelSpeed = 6f;
        BaseDamage = 100;
        IsAutoTarget = false;
    }
    
    private void Update()
    {
        if (Time.time > Timestamp && Input.GetKeyDown(KeyCode.Alpha2))
        {
            StartCoroutine(nameof(DoSpell));
        }
    }

    protected override IEnumerator DoSpell()
    {
        Timestamp = Time.time + Cooldown;
        var playerTransform = transform;
        var playerForward = playerTransform.forward;

        StartCoroutine(nameof(AnimatePlayer));
        
        var spell = Instantiate(_initialSummon, playerTransform.position, playerTransform.rotation * Quaternion.Euler(0 ,180, 0));
        var rbSpell = spell.gameObject.GetComponent<Rigidbody>();
        
        spell.transform.Translate(Vector3.forward + Vector3.up);
        rbSpell.velocity =  playerForward * TravelSpeed;
        Destroy(spell.gameObject, Range);
        yield break;
    }

    protected override IEnumerator AnimatePlayer()
    {
        _playerAnimator.AnimateSpell(Name);
        yield return new WaitForSeconds(0.1f);
        _playerAnimator.StopAnimatingSpell(Name);
    }
}
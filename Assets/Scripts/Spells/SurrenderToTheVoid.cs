using System.Collections;
using UnityEngine;

public class SurrenderToTheVoid : Spells
{
    [SerializeField] private Material voidMaterial;
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private GameObject initialAutoAttack;
    [SerializeField] private GameObject initialExplosion;
    [SerializeField] private GameObject voidAutoAttack;
    [SerializeField] private GameObject voidExplosion;

    private CharacterCombat _characterCombat;
    private float _initialBaseDamage;
    private Fireball _fireballScript;
    private GameObject _fireball;
    private ParticleSystem _voidEffect;
    

    private float Duration { get; set; }
    private void Awake()
    {
        Name = "Surrender to the Void";
        Description = "The mage turns to the Void, dealing bonus damage on Auto Attack and restoring Mana on hit.";
        cooldown = 20f;
        IsAutoTarget = true;
        hitRadius = 2f;
        BaseDamage = 200f;
        _voidEffect = transform.Find("VoidEffect").GetComponent<ParticleSystem>();
        Duration = 10f;
        _fireballScript = GetComponent<Fireball>();
        _initialBaseDamage = _fireballScript.BaseDamage;
        _characterCombat = GetComponent<CharacterCombat>();
    }

    private void Update()
    {
        if (Time.time > Timestamp && Input.GetKeyDown(KeyCode.Alpha4))
        {
            StartCoroutine(nameof(DoSpell));
        }
    }

    protected override IEnumerator DoSpell()
    {
        Timestamp = Time.time + cooldown;
        if(!_voidEffect.isPlaying)_voidEffect.Play(true);
        BuffAutoAttack();
        
        // TODO: Refactor into method to make it DRY and reusable
        Renderer[] rs = GetComponentsInChildren<Renderer>();
        foreach (Renderer r in rs)
        {
            if(!r.gameObject.name.Equals("VoidEffect") && !r.gameObject.name.Equals("CenterVoid"))
                r.material = voidMaterial;
        }

        StartCoroutine(nameof(AnimatePlayer));
        yield return new WaitForSeconds(Duration);
        
        // TODO: Refactor into method to make it DRY and reusable
        foreach (Renderer r in rs)
        {
            if(!r.gameObject.name.Equals("VoidEffect") && !r.gameObject.name.Equals("CenterVoid"))
                r.material = defaultMaterial;
        }
        if(_voidEffect.isPlaying)_voidEffect.Stop(true);
        DebuffAutoAttack();
    }

    protected override IEnumerator AnimatePlayer()
    {
        _playerAnimator.AnimateSpell(Name);
        yield return new WaitForSeconds(0.1f);
        _playerAnimator.StopAnimatingSpell(Name);
    }

    private void BuffAutoAttack()
    {
        _fireballScript.BaseDamage = BaseDamage;
        _fireballScript.damageSender = voidAutoAttack;
        _fireballScript.explosion = voidExplosion;
    }

    private void DebuffAutoAttack()
    {
        _fireballScript.BaseDamage = _initialBaseDamage;
        _fireballScript.damageSender = initialAutoAttack;
        _fireballScript.explosion = initialExplosion;
    }
}
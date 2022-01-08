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

    private GameObject _fireball;
    private Vector3 _initialScale;
    private ParticleSystem _voidEffect;

    private float Duration { get; set; }
    private void Awake()
    {
        Name = "Surrender to the Void";
        Description = "The mage turns to the power of the void. He cannot cast spells but all his Auto Attacks becomes void, piercing through armor and dealing more damage. After Surrender to the void, the mage gains all his mana back.";
        Cooldown = 20f;
        IsAutoTarget = true;
        _voidEffect = transform.Find("VoidEffect").GetComponent<ParticleSystem>();
        Duration = 10f;
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
        Timestamp = Time.time + Cooldown;
        if(!_voidEffect.isPlaying)_voidEffect.Play(true);
        GetComponent<Fireball>().fireball = voidAutoAttack;
        GetComponent<Fireball>().explosion = voidExplosion;
        Renderer[] rs = GetComponentsInChildren<Renderer>();
        foreach (Renderer r in rs)
        {
            if(!r.gameObject.name.Equals("VoidEffect") && !r.gameObject.name.Equals("CenterVoid"))
                r.material = voidMaterial;
        }

        StartCoroutine(nameof(AnimatePlayer));
        yield return new WaitForSeconds(Duration);   
        foreach (Renderer r in rs)
        {
            if(!r.gameObject.name.Equals("VoidEffect") && !r.gameObject.name.Equals("CenterVoid"))
                r.material = defaultMaterial;
        }
        if(_voidEffect.isPlaying)_voidEffect.Stop(true);
        GetComponent<Fireball>().fireball = initialAutoAttack;
        GetComponent<Fireball>().explosion = initialExplosion;

    }

    protected override IEnumerator AnimatePlayer()
    {
        _playerAnimator.AnimateSpell(Name);
        yield return new WaitForSeconds(0.1f);
        _playerAnimator.StopAnimatingSpell(Name);
    }
}
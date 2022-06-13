using System.Collections;
using UnityEngine;

public class Cleave : ThrowSpells
{
    private ParticleSystem _cleaveEffect;
    [SerializeField]private GameObject shadowWarrior;
    private CleaveScript _cleaveScript;
    private AutoAttack _autoAttack;
    private float Duration { get; set; }
    
    private void Awake()
    {
        _cleaveEffect = transform.Find("CleaveEffect").GetComponent<ParticleSystem>();
        _autoAttack = GetComponent<AutoAttack>();
        Name = "Cleave";
        Description = "You cleave around you. For 8 seconds, all attacks does half the damage to the enemies hit.";
        cooldown = 1f;
        BaseDamage = 500f;
        hitRadius = 2f;
        Duration = 8f;
    }
    
    private void Update()
    {
        if (Time.time > Timestamp && Input.GetKeyDown(KeyCode.Alpha4))
        {
            StartCoroutine(nameof(DoSpell));
        }
    }

    public void StopSpreading()
    {
        _autoAttack.spreadAttack = null;
    }
    
    protected override IEnumerator DoSpell()
    {
        Timestamp = Time.time + cooldown;
        _playerAnimator.AnimateSpell(Name);
        SpawnShadowWarrior();
        SpawnCleave();
        Invoke(nameof(StopSpreading), Duration);
        if(!_cleaveEffect.isPlaying)_cleaveEffect.Play(false);
        yield return  new WaitForSeconds(0.1f);
        _playerAnimator.StopAnimatingSpell(Name);
    }

    private void SpawnShadowWarrior()
    {
        Vector3 spawnDistance = new Vector3(0,0,-1);
        Vector3 spawnPoint = transform.TransformPoint(spawnDistance);
        var cloneShadow = Instantiate(shadowWarrior, spawnPoint,
            transform.rotation * Quaternion.Euler(0, 180, 0));
        cloneShadow.transform.localScale = transform.localScale;
    }

    private void SpawnCleave()
    {
        Transform playerTransform = transform;
        var cloneCleave = Instantiate(_initialSummon, playerTransform.position, Quaternion.identity, playerTransform);
        Destroy(cloneCleave, 0.1f);
    }
}

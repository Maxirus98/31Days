using System.Collections;
using UnityEngine;

public class Cleave : Spells
{
    private ParticleSystem _cleaveEffect;
    [SerializeField]private GameObject shadowWarrior;

    private void Awake()
    {
        _cleaveEffect = transform.Find("CleaveEffect").GetComponent<ParticleSystem>();
        Name = "Cleave";
        Description = "You cleave in front of you. For 8 seconds, all enemies hit will take damage from your attacks.";
        Cooldown = 1f;
        BaseDamage = 10;
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
        _playerAnimator.AnimateSpell(Name);
        SpawnShadowWarrior();
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
    }
}

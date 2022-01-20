using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Growth : Spells
{
    private Vector3 _initialScale;
    private ParticleSystem _growthEffect;
    private float _duration;
    private float _growthScale;
    private void Awake()
    {
        Name = "Growth";
        Description = "For a limited time, you grow in size and in get bonus health";
        cooldown = 1f;
        IsAutoTarget = true;
        _initialScale = transform.localScale;
        _growthEffect = transform.Find("GrowthEffect").GetComponent<ParticleSystem>();
        _duration = 5f;
        _growthScale = 1.5f;
    }

    private void Update()
    {
        if (Time.time >= Timestamp && Input.GetKeyDown(KeyCode.Alpha2))
        {
            StartCoroutine(nameof(DoSpell));

        }
    }


    protected override IEnumerator DoSpell()
    {
        if(!_growthEffect.isPlaying)_growthEffect.Play(false);
        StartCoroutine(nameof(AnimatePlayer));
        Timestamp = Time.time + cooldown;
        transform.localScale *= _growthScale;
        yield return new WaitForSeconds(_duration);    
        transform.localScale = _initialScale;
        if(_growthEffect.isPlaying)_growthEffect.Stop(false);
    }


    protected override IEnumerator AnimatePlayer()
    {
        Debug.Log("animate player with spellName" + Name);
        _playerAnimator.AnimateSpell(Name);
        yield return new WaitForSeconds(0.1f);
        _playerAnimator.StopAnimatingSpell(Name);
    }
}

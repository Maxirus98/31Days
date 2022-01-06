using System.Collections;
using UnityEngine;

public class SurrenderToTheVoid : Spells
{
    private Vector3 _initialScale;
    private ParticleSystem _voidEffect;
    private float Duration { get; set; }
    private void Awake()
    {
        Name = "Surrender to the Void";
        Description = "The mage turns to the power of the void. He cannot cast spells but all his Auto Attacks becomes void, piercing through armor and dealing more damage. After Surrender to the void, the mage gains all his mana back.";
        Cooldown = 1f;
        IsAutoTarget = true;
        _voidEffect = transform.Find("MageVoid").GetComponent<ParticleSystem>();
        Duration = 10f;
    }

    private void Update()
    {
        if (Time.time >= Timestamp && Input.GetKeyDown(KeyCode.Alpha4))
        {
            StartCoroutine(nameof(DoSpell));
        }
    }


    protected override IEnumerator DoSpell()
    {
        Timestamp = Time.time + Cooldown;
        if(!_voidEffect.isPlaying)_voidEffect.Play(true);
        StartCoroutine(nameof(AnimatePlayer));
        yield return new WaitForSeconds(Duration);    
        if(_voidEffect.isPlaying)_voidEffect.Stop(true);
    }


    protected override IEnumerator AnimatePlayer()
    {
        Debug.Log("animate player with spellName" + Name);
        _playerAnimator.AnimateSpell(Name);
        yield return new WaitForSeconds(0.1f);
        _playerAnimator.StopAnimatingSpell(Name);
    }
}
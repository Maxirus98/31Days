using System.Collections;
using UnityEngine;

public class ThunderShout : Spells
{
    private ParticleSystem _cloudEffect;
    private bool _isSpreading = false;
    private ParticleSystem.ShapeModule _shape;
    private void Awake()
    {
        _cloudEffect = transform.Find("ThunderShout").GetComponent<ParticleSystem>();
        _shape = _cloudEffect.shape;
        Name = "Thunder Shout";
        Description = "You shout and form a cloud that targets all enemies. All your other attacks will spread to them. Target lasts for 8 sec";
        Cooldown = 1f;
        BaseDamage = 10;
    }
    private void Update()
    {
        if (Time.time > Timestamp && Input.GetKeyDown(KeyCode.Alpha4))
        {
            StartCoroutine(nameof(DoSpell));
        }
        if(_isSpreading)
            SpreadCloudRadius();
        if(!_isSpreading)
            ResetCloudRadius();
    }

    protected override IEnumerator DoSpell()
    {
        Timestamp = Time.time + Cooldown;
        StartCoroutine(nameof(AnimatePlayer));
        if(!_cloudEffect.isPlaying)_cloudEffect.Play(false);
        _isSpreading = true;
        yield return new WaitForSeconds(1f);
        _isSpreading = false;
        if(_cloudEffect.isPlaying)_cloudEffect.Stop(false);

    }

    private void SpreadCloudRadius()
    {
        _shape.radius += 5f * Time.deltaTime;
    }

    private void ResetCloudRadius()
    {
        _shape.radius = 1f;
    }
    
    protected override IEnumerator AnimatePlayer()
    {
        Debug.Log("animate player with spellName" + Name);
        _playerAnimator.AnimateSpell(Name);
        yield return new WaitForSeconds(0.1f);
        _playerAnimator.StopAnimatingSpell(Name);
    }
    
}

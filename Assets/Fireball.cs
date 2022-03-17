using UnityEngine;
using System.Collections;

public class Fireball : AutoTargetSpell
{
    public GameObject explosion;
    private float _autoAttackClipLenght;
    
    private void Awake()
    {
        cooldown = 2f;
        Name = "AutoAttacking";
        Range = 150f;
        BaseDamage = 150f;
        _autoAttackClipLenght = GetAnimationClipLength();
    }

    private void Update()
    {
        base.Update();
        if (Time.time >= Timestamp)
        {
            StartCoroutine(nameof(DoSpell));
        }

        if (cloneDmgSender && MouseManager.focus)
        {
            var fireballPos = cloneDmgSender.transform.position;
            var focusTransform = MouseManager.focus.transform;
            if (Vector3.SqrMagnitude(cloneDmgSender.transform.position - (focusTransform.position + Vector3.up * 
                focusTransform.localScale.y)) < focusTransform.localScale.y)
            {
                Instantiate(explosion, fireballPos, transform.rotation, cloneDmgSender.transform);
                Destroy(cloneDmgSender, 0.2f);
            }
        }
    }

    protected override IEnumerator DoSpell()
    {
        if (IsEnemyInRange(Range))
        {
            Timestamp = Time.time + cooldown;
            DamageDid = false;
            StartCoroutine(nameof(AnimatePlayer));
            yield return new WaitForSeconds(_autoAttackClipLenght * Time.deltaTime);
            CastFireball();
        }
        else
        {
            _playerAnimator.StopAnimatingSpell(Name);
        }
    }

    protected override IEnumerator AnimatePlayer()
    {
        _playerAnimator.AnimateSpell(Name);
        yield return new WaitForSeconds(0.5f);
        _playerAnimator.StopAnimatingSpell(Name);
    }

    private float GetAnimationClipLength()
    {
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (!clip.name.Equals("AutoAttacking"))
            {
                return clip.length;
            }
        }

        return 0f;
    }

    private void CastFireball()
    {
        var spawnPoint = AbilityUtils.FindDeepChild("SpellCast", transform);
        cloneDmgSender = Instantiate(damageSender, spawnPoint.position, spawnPoint.rotation);
    }
}
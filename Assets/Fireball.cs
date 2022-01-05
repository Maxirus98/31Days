using UnityEngine;
using System.Collections;


// TODO : Utiliser l'héritage avec AutoAttack
public class Fireball : AutoTargetSpell
{
    [SerializeField]private GameObject explosion;
    [SerializeField]private GameObject fireball;
    private GameObject _cloneFireball;
    public static bool Blocked { get; set; }
    private float _autoAttackClipLenght;
    
    public void Awake()
    {
        Cooldown = 2f;
        Name = "AutoAttacking";
        Range = 150f;
        Blocked = false;
        _autoAttackClipLenght = GetAnimationClipLength();
    }

    private void Update()
    {
        if (Time.time >= Timestamp)
        {
            StartCoroutine(nameof(DoSpell));
        }
        
        if (_cloneFireball && MouseManager.focus)
        {
            var fireballPosition = _cloneFireball.transform.position;
            var focusTransform = MouseManager.focus.transform;
            _cloneFireball.transform.position = Vector3.MoveTowards(fireballPosition,
                focusTransform.position + Vector3.up * 
                focusTransform.localScale.y, 20f * Time.deltaTime);
            if (Vector3.SqrMagnitude(fireballPosition - focusTransform.position) < 2f)
            {
                Destroy(_cloneFireball, 1f);
                Instantiate(explosion, _cloneFireball.transform.position, _cloneFireball.transform.rotation,
                    _cloneFireball.transform);
            }
        }
    }

    protected override IEnumerator DoSpell()
    {
        if (IsInRange(Range))
        {
            Timestamp = Time.time + Cooldown;
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
        AnimationClip[] clips = _animator.runtimeAnimatorController.animationClips;
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
        var spawnPoint = MageSpell.FindDeepChild("SpellCast", transform);
        _cloneFireball = Instantiate(fireball, spawnPoint.position, spawnPoint.rotation);
    }
}
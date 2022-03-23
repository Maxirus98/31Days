using System.Collections;
using UnityEngine;

/// <summary>
/// BleedDebuff is on the GameObject BleedDebuff
/// </summary>
public class BleedDebuff : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private float damagePerTick;

    private readonly string BLEED_DEBUFF_EFFECT = "BleedDebuffEffect";
    private CharacterCombat _characterCombat;
    private ParticleSystem _particle;
    private float TimeStamp { get; set; }
    private float RepeatRate { get; set; } = 1f;
    private bool IsBleeding { get; set; }

    private void Start()
    {
        _characterCombat = GetComponentInParent<CharacterCombat>();
        _particle = transform.Find(BLEED_DEBUFF_EFFECT).GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (IsBleeding)
        {
            StartCoroutine(StopBleeding());
            if(Time.time >= TimeStamp)
            {
                Bleed();
            }
        }
    }
    
    /// <summary>
    /// Trigger to make the target bleed and take damage.
    /// </summary>
    public void StartBleeding()
    {
        if(!_particle.isPlaying)_particle.Play();
        IsBleeding = true;
    }

    /// <summary>
    /// Make the target bleed for a duration
    /// </summary>
    private void Bleed()
    {
        _characterCombat.TakeDamageNoAnimation(damagePerTick);
        TimeStamp = Time.time + RepeatRate;
    }

    /// <summary>
    /// Coroutine to make the target stop bleeding after duration
    /// </summary>
    /// <returns>IEnumerator for Couroutine</returns>
    private IEnumerator StopBleeding()
    {
        yield return new WaitForSeconds(duration);
        if(_particle.isPlaying)_particle.Stop();
        IsBleeding = false;
    }
}

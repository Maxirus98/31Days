using System.Collections;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Charge : AutoTargetSpell
{
    private ParticleSystem _chargeEffect;
    private float _stopChargingRange;
    private bool _isCharging;
    private float _chargeSpeed;
    
    private void Awake()
    {
        _chargeEffect = transform.Find("ChargeEffect").GetComponent<ParticleSystem>();
        Name = "Charge";
        Description = "You charge forward hitting and stunning the enemy";
        Cooldown = 1f;
        Range = 200f;
        _stopChargingRange = 2f;
        _chargeSpeed = 20f;
        BaseDamage = 10f;
        IsAutoTarget = true;
    }

    private void Update()
    {
        if (Time.time > Timestamp && Input.GetKeyDown(KeyCode.Alpha3))
        {
            StartCoroutine(nameof(DoSpell));
        }
        
        if (_isCharging && MouseManager && MouseManager.focus)
        {
            StartCharging();
        }
        StopCharging();
    }

    protected override IEnumerator DoSpell()
    {
        if (IsInRange(Range))
        {
            Timestamp = Time.time + Cooldown;
            _isCharging = true;
        }   
        yield break;
    }

    private void StartCharging()
    {
        RaycastHit hit;
        Vector3 fromPosition = transform.position;
        Vector3 toPosition = MouseManager.focus.transform.position;
        Vector3 direction = toPosition - fromPosition;
    
        Debug.DrawLine(transform.position, direction);
        if(Physics.Linecast(transform.position,direction,out hit))
        { 
            transform.rotation = Quaternion.LookRotation(direction);
            _playerAnimator.AnimateSpell(Name);
            if(!_chargeEffect.isPlaying)_chargeEffect.Play(false);
            var focusPosition = MouseManager.focus.transform.position;
            transform.position = Vector3.MoveTowards(transform.position, focusPosition,  _chargeSpeed * Time.deltaTime);
        }
    }

    private void StopCharging()
    {
        if (IsInRange(_stopChargingRange) && _isCharging)
        {
            
            _isCharging = false;
            if (_chargeEffect.isPlaying) _chargeEffect.Stop(false);
            _playerAnimator.StopAnimatingSpell(Name);
        }
    }
}

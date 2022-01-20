using System.Collections;
using UnityEngine;

public class ChannelingFire : Spells
{
    [SerializeField] private GameObject channelingFire;
    private GameObject _cloneChannelingFire;
    private float CastTime { get; set; }
    private float Duration { get; set; }

    private PlayerController _playerController;
    private readonly float YAW_SPEED = 20f;

    private void Awake()
    {
        Name = "Channeling Fire";
        Description = "Channel a beam of fire in front of you. (10 mana by second).";
        cooldown = 8f;
        Duration = 6f;
        CastTime = 1f;
        _playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (Time.time > Timestamp && Input.GetKeyDown(KeyCode.Alpha1))
        {
            StartCoroutine(nameof(DoSpell));
        }
    }

    protected override IEnumerator DoSpell()
    {
        Timestamp = Time.time + cooldown;
        _playerController.YawSpeed = YAW_SPEED;
        StartCoroutine(nameof(AnimatePlayer));
        Invoke(nameof(CastChannelingFire), CastTime);
        yield return new WaitForSeconds(Duration);
        _playerController.ResetYawSpeed();
    }
    
    protected override IEnumerator AnimatePlayer()
    {
        _playerAnimator.AnimateSpell(Name);
        yield return new WaitForSeconds(CastTime);
        _playerAnimator.StopAnimatingSpell(Name);
    }

    private void CastChannelingFire()
    {
        _playerController.YawSpeed = YAW_SPEED;
        var spawnPoint = AbilityUtils.FindDeepChild("SpellCast", transform);
        _cloneChannelingFire = Instantiate(channelingFire, spawnPoint.position, spawnPoint.rotation * Quaternion.Euler(180, 0, 0), spawnPoint.transform);
        Destroy(_cloneChannelingFire, Duration);
    }
    
    
    
}
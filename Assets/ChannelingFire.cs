using System.Collections;
using UnityEngine;

public class ChannelingFire : Spells
{
    public readonly float DAMAGE_INTERVAL = 0.5f;
    [SerializeField] private GameObject channelingFire;
    private GameObject _cloneChannelingFire;
    private Fireball _fireball;
    private float CastTime { get; set; }
    private float Duration { get; set; }

    private PlayerController _playerController;
    private readonly float YAW_SPEED = 20f;
    private readonly float SPEED = 0f;
    private Transform _spawnPoint;

    private void Awake()
    {
        Name = "Channeling Fire";
        Description = "Channel a beam of fire in front of you.";
        cooldown = 8f;
        Duration = 6f;
        CastTime = 1f;
        BaseDamage = 100f;
        _playerController = GetComponent<PlayerController>();
        _fireball = GetComponent<Fireball>();
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
        _fireball.Blocked = true;
        _playerController.Speed = SPEED;
        _playerController.YawSpeed = YAW_SPEED;
        StartCoroutine(nameof(AnimatePlayer));
        Invoke(nameof(CastChannelingFire), CastTime);
        yield return new WaitForSeconds(Duration + CastTime);
        Reset();
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
        _spawnPoint = AbilityUtils.FindDeepChild("SpellCast", transform);
        _cloneChannelingFire = Instantiate(channelingFire, _spawnPoint.position, _spawnPoint.rotation * Quaternion.Euler(180, 0, 0), _spawnPoint.transform);
        Destroy(_cloneChannelingFire, Duration);
    }

    private void Reset()
    {
        _fireball.Blocked = false;
        _playerController.Speed = _playerController.INIT_SPEED;
        _playerController.YawSpeed = _playerController.INIT_YAW_SPEED;
    }
    
    
}
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
        Description = "Channel a beam of fire in front of you, dealing damage in a line.";
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
        }else if (Input.GetAxis("Vertical") > 0f)
        {
            CancelChannelingFire();
        }
    }

    protected override IEnumerator DoSpell()
    {
        Timestamp = Time.time + cooldown;
        _playerController.Speed = SPEED;
        _playerController.YawSpeed = YAW_SPEED;
        StartCoroutine(nameof(AnimatePlayer));
        Invoke(nameof(CastChannelingFire), CastTime);
        yield return new WaitForSeconds(Duration + CastTime);
        Reset();
    }
    
    protected override IEnumerator AnimatePlayer()
    {
        playerAnimator.AnimateSpell(Name);
        yield return new WaitForSeconds(Duration + CastTime);
        playerAnimator.StopAnimatingSpell(Name);
    }

    private void CastChannelingFire()
    {
        _playerController.YawSpeed = YAW_SPEED;
        _spawnPoint = AbilityUtils.FindDeepChild("SpellCast", transform);
        _cloneChannelingFire = Instantiate(channelingFire, _spawnPoint.position, transform.rotation, _spawnPoint);
        Destroy(_cloneChannelingFire, Duration - CastTime / 2);
    }

    private void CancelChannelingFire()
    {
        playerAnimator.StopAnimatingSpell(Name);
        Reset();
        if(_cloneChannelingFire) Destroy(_cloneChannelingFire);
    }

    private void Reset()
    {
        _playerController.Speed = _playerController.INIT_SPEED;
        _playerController.YawSpeed = _playerController.INIT_YAW_SPEED;
    }
    
    
}
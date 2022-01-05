using System.Collections;
using UnityEngine;

public class ChannelingFire : Spells
{
    [SerializeField] private GameObject channelingFire;
    private GameObject _cloneChannelingFire;
    private float WarmUpTime { get; set; }
    private float Duration { get; set; }

    private PlayerController _playerController;
    private readonly float YAW_SPEED = 20f;

    private void Awake()
    {
        Name = "Channeling Fire";
        Description = "Channel a beam of fire in front of you. (10 mana by second).";
        Cooldown = 8f;
        Duration = 6f;
        WarmUpTime = 1f;
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
        _playerController.YawSpeed = YAW_SPEED;
        StartCoroutine(nameof(AnimatePlayer));
        Invoke(nameof(CastChannelingFire), WarmUpTime);
        yield return new WaitForSeconds(Duration);
        _playerController.ResetYawSpeed();
    }
    
    protected override IEnumerator AnimatePlayer()
    {
        _playerAnimator.AnimateSpell(Name);
        yield return new WaitForSeconds(WarmUpTime);
        _playerAnimator.StopAnimatingSpell(Name);
    }

    private void CastChannelingFire()
    {
        _playerController.YawSpeed = YAW_SPEED;
        var spawnPoint = MageSpell.FindDeepChild("SpellCast", transform);
        _cloneChannelingFire = Instantiate(channelingFire, spawnPoint.position, spawnPoint.rotation * Quaternion.Euler(180, 0, 0), spawnPoint.transform);
        Destroy(_cloneChannelingFire, Duration);
    }
    
    
    
}
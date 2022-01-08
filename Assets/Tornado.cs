using System.Collections;
using UnityEngine;

public class Tornado : SkillShotSpell
{
    [SerializeField] private GameObject tornado;
    [SerializeField] private GameObject spawn;
    private Transform _abilityRange;

    private GameObject _cloneSpawn;
    private Vector3 _spawnPoint;
    private GameObject _cloneTornado;
    private float CastTime { get; set; }
    private float Duration { get; set; }
    private void Awake()
    {
        Name = "Tornado";
        Duration = 5f;
        Description = $"Summon a tornado and bring all enemies in the center. They are unable to move. (Last {Duration} seconds). (10 mana)";
        Cooldown = 1f;
        CastTime = 1f;
        MaxRange = 10f;
        AreaScale = new Vector3(MaxRange, 0.05f, MaxRange);
        _abilityRange = gameObject.transform.Find("AbilityRange");
    }

    private void Update()
    {
        if (Time.time > Timestamp && Input.GetKeyDown(KeyCode.Alpha3) && !_cloneSpawn)
        {
            ToggleAbilityRange(true, _abilityRange.gameObject);
            SetAreaScale(_abilityRange);
            InstantiateSpawnPoint();
        }
        if (Input.GetMouseButtonDown(0) && _cloneSpawn)
        {
            StartCoroutine(nameof(DoSpell));
        }
        else if (Input.GetMouseButtonDown(1) && _cloneSpawn)
        {
            ToggleAbilityRange(false, _abilityRange.gameObject);
            Destroy(_cloneSpawn);
        }
    }

    protected override IEnumerator DoSpell()
    {
        Timestamp = Time.time + Cooldown;
        StartCoroutine(nameof(AnimatePlayer));
        CastTornado();
        DestroyMarkers();
        yield return new WaitForSeconds(Duration);
    }
    
    protected override IEnumerator AnimatePlayer()
    {
        _playerAnimator.AnimateSpell(Name);
        yield return new WaitForSeconds(CastTime);
        _playerAnimator.StopAnimatingSpell(Name);
    }

    private void InstantiateSpawnPoint()
    {
        _cloneSpawn = Instantiate(spawn, transform.position, transform.rotation);
        spawnArea = _cloneSpawn;
    }

    private void DestroyMarkers()
    {
        ToggleAbilityRange(false, _abilityRange.gameObject);
        Destroy(_cloneSpawn);
    }

    private void CastTornado()
    {
        _cloneTornado = Instantiate(tornado, _cloneSpawn.transform.position - new Vector3(0, 3, 0), _cloneSpawn.transform.rotation);
        Destroy(_cloneTornado, Duration);
    }
}
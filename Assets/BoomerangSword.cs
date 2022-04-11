using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BoomerangSword : ThrowSpells
{
    public GameObject indicator;

    private readonly string BLADESTORM_TITLE = "Bladestorm";
    private GameObject _cloneIndicator;
    private GameObject _cloneSword;
    private Transform _playerTransform;
    private Vector3 _targetPos;
    private Vector3 _initPos;

    private Memory _bladestorm;
    private GameObject _bladestormGo;
    
    private void Awake()
    {
        Name = "Boomerang Sword";
        TravelSpeed = 6f;
        Description = "You throw your sword in front of you and it comes back at you dealing damage and slowing enemies in its path";
        cooldown = 1f;
        TravelTime = 1f;
        BaseDamage = 100;
        IsAutoTarget = false;
        _playerTransform = transform;
    }
    
    private void Update()
    {
        // if Memory : Bladestorm is not selected
        if (_bladestorm == null)
        {
            _bladestorm = Memories.GetChosenMemoryByTitle(BLADESTORM_TITLE);
            _bladestormGo = AbilityUtils.FindDeepChild(BLADESTORM_TITLE, transform).gameObject;
            _bladestormGo.SetActive(false);
            if (Input.GetKeyDown(KeyCode.Alpha1) && !_cloneIndicator)
            {
                SpawnIndicator();
            }
        }
        else
        {
            StartCoroutine(Bladestorm());
        }

        if (Time.time > Timestamp && _cloneIndicator && Input.GetMouseButton(0))
        {
            StartCoroutine(nameof(DoSpell));
            Destroy(_cloneIndicator);
        }

        if (_cloneIndicator && Input.GetMouseButton(1))
        {
            Destroy(_cloneIndicator);
        }
    }

    private void SpawnIndicator()
    {
        var playerPos = _playerTransform.position;
        var playerDirection = _playerTransform.forward;
        var playerRotation = _playerTransform.rotation;
        var spawnPos = playerPos + playerDirection * indicator.transform.localScale.z * _playerTransform.localScale.z;
        
        _cloneIndicator = Instantiate(indicator, spawnPos + Vector3.up * 0.1f, playerRotation, _playerTransform);
    }

    protected override IEnumerator DoSpell()
    {
        Timestamp = Time.time + cooldown;
        var playerTransform = transform;

        StartCoroutine(nameof(AnimatePlayer));

        _cloneSword = Instantiate(_initialSummon, _playerTransform.position, playerTransform.rotation);
        _cloneSword.transform.Rotate(Vector3.right * 90);

        yield return new WaitForSeconds(TravelTime);
    }

    protected override IEnumerator AnimatePlayer()
    {
        Debug.Log("animate player with spellName" + Name);
        _playerAnimator.AnimateSpell(Name);
        yield return new WaitForSeconds(0.1f);
        _playerAnimator.StopAnimatingSpell(Name);
    }

    // if Memory: Bladestorm is selected
    // TODO: Memory checked, gameobject with script that checks if the memory is selected.
    // TODO: Spells are strongly coupled when they need to interact with each other, find a way to make it easier to change spells
    private IEnumerator Bladestorm()
    {
        if (sprite != _bladestorm.sprite)
        {
            print("changed SPRITE TO BLADESTORM");
            spellBar.transform.GetChild(spellSlot).Find("BackgroundSprite").GetComponent<Image>().sprite = _bladestorm.sprite;
            sprite = _bladestorm.sprite;
            Description = _bladestorm.description;
            Name = _bladestorm.title;
            cooldown = 5f;
        }

        if (!(Time.time > Timestamp) || !Input.GetKeyDown(KeyCode.Alpha1)) yield break;
        _bladestormGo.SetActive(true);
        _playerAnimator.AnimateSpell(BLADESTORM_TITLE);
        yield return new WaitForSeconds(cooldown);
        _playerAnimator.StopAnimatingSpell(BLADESTORM_TITLE);
        _bladestormGo.SetActive(false);
    }
}

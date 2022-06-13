using System;
using System.Collections;
using System.Linq;
using UnityEngine;

/// <summary>
/// BoomerangSwordScript is attached to the BoomerangSword that is thrown so its MonoBehaviour
/// only starts when it's instantiated.
/// </summary>
public class BoomerangSwordScript : MonoBehaviour
{
    public BoomerangSword boomerangSword;
    
    [SerializeField] private LayerMask enemyLayers;
    
    // Memories
    private readonly string BLEED_DEBUFF = "BleedDebuff";
    private readonly string BLEED_TITLE = "Bleed";
    private readonly string JUSTICE_BUFF = "HammerOfJustice";
    private readonly string JUSTICE_TITLE = "Hammer Of Justice";
    
    private readonly float HIT_RADIUS = 1f;
    private readonly string PLAYER_TAG = "Player";
    private readonly string PLAYER_SWORD = "WarriorSword";
    
    private GameObject _player;
    private bool _isMoving;
    private Vector3 _locationInFrontOfPlayer;
    private MeshRenderer _sword;
    
    private Memory _bleed;
    private Memory _justice;
    private GameObject _hammerOfJustice;

    private void Start()
    {
        _player = GameObject.FindWithTag(PLAYER_TAG);
        boomerangSword = _player.GetComponent<BoomerangSword>();
        _isMoving = false;
        _locationInFrontOfPlayer = _player.transform.position + Vector3.up +
                                   _player.transform.forward * (boomerangSword.indicator.transform.localScale.z + 2f);

        _sword = AbilityUtils.FindDeepChild(PLAYER_SWORD, _player.transform).GetComponent<MeshRenderer>();
        
        _sword.enabled = false;
        StartCoroutine(MoveSword());
        _bleed = Memories.GetChosenMemoryByTitle(BLEED_TITLE);
        _justice = Memories.GetChosenMemoryByTitle(JUSTICE_TITLE);
        if (_justice != null)
        {
            _hammerOfJustice = BuffsParent.Buffs.First(go => go.name.Equals(JUSTICE_BUFF));
            _hammerOfJustice.SetActive(true);
        }
    }

    private void Update()
    {
        if (_isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position,_locationInFrontOfPlayer, Time.deltaTime * 15); //Change The Position To The Location In Front Of The Player            
        }

        if (!_isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(_player.transform.position.x,_player.transform.position.y + 1,_player.transform.position.z), Time.deltaTime * 15); //Return To Player
        }
        
        if(!_isMoving && Vector3.SqrMagnitude(_player.transform.position - transform.position) <= 1f)
        {
            _sword.enabled = true;
            Destroy(gameObject);
        }
    }

    private IEnumerator MoveSword()
    {
        _isMoving = true;
        yield return new WaitForSeconds(boomerangSword.TravelTime);
        _isMoving = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Enemy")) return;
        var enemy = other.GetComponent<CharacterCombat>();
        enemy.TakeDamage(boomerangSword.BaseDamage);
        
        // Check in player memories component if BleedMemory is chosen
        if (_bleed != null)
        {
            var bleedDebuff = AbilityUtils.FindDeepChild(BLEED_DEBUFF, enemy.transform);
            if(bleedDebuff)bleedDebuff.GetComponent<BleedDebuff>().StartBleeding();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, HIT_RADIUS);
    }
}

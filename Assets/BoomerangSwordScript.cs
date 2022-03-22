using System.Collections;
using UnityEngine;

public class BoomerangSwordScript : MonoBehaviour
{
    private readonly float HitRadius = 1f;
    [SerializeField] private LayerMask enemyLayers;
    private GameObject _player;
    public BoomerangSword boomerangSword;
    private bool _isMoving;
    private Vector3 _locationInFrontOfPlayer;
    private MeshRenderer _sword;
    private BleedMemory _bleedMemory;

    private void Start()
    {
        _isMoving = false;
        _player = GameObject.FindWithTag("Player");
        boomerangSword = _player.GetComponent<BoomerangSword>();

        _locationInFrontOfPlayer = _player.transform.position + Vector3.up +
                                   _player.transform.forward * (boomerangSword.indicator.transform.localScale.z + 2f);

        _sword = AbilityUtils.FindDeepChild("WarriorSword", _player.transform).GetComponent<MeshRenderer>();
        
        _sword.enabled = false;
        StartCoroutine(MoveSword());
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
        // Check if Memories
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, HitRadius);
    }
}

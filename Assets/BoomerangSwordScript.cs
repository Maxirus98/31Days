using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangSwordScript : MonoBehaviour
{
    private readonly float HitRadius = 1f;
    [SerializeField] private LayerMask enemyLayers;
    private GameObject _player;
    private BoomerangSword _boomerangSword;
    private List<Collider> _collidersCollidedWith = new List<Collider>();
    private bool _isMoving;
    private Vector3 _locationInFrontOfPlayer;
    private MeshRenderer _sword;
    
    void Start()
    {
        _isMoving = false;
        _player = GameObject.FindWithTag("Player");
        _boomerangSword = _player.GetComponent<BoomerangSword>();
        _boomerangSword.boomerangSwordScript = this;

        _locationInFrontOfPlayer = _player.transform.position + Vector3.up +
                                   _player.transform.forward * (_boomerangSword.indicator.transform.localScale.z + 2f);

        _sword = AbilityUtils.FindDeepChild("WarriorSword", _player.transform).GetComponent<MeshRenderer>();
        
        _sword.enabled = false;
        StartCoroutine(Boom());
    }
    
    void Update()
    {
        DamageEnemiesHit();
        
        if (_isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position,_locationInFrontOfPlayer, Time.deltaTime * 15); //Change The Position To The Location In Front Of The Player            
        }

        if (!_isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(_player.transform.position.x,_player.transform.position.y + 1,_player.transform.position.z), Time.deltaTime * 15); //Return To Player
        }
        print(Vector3.SqrMagnitude(_player.transform.position - transform.position));
        if(!_isMoving && Vector3.SqrMagnitude(_player.transform.position - transform.position) <= 1f)
        {
            _sword.enabled = true;
            Destroy(gameObject);
        }
    }
    
    IEnumerator Boom()
    {
        _isMoving = true;
        yield return new WaitForSeconds(_boomerangSword.TravelTime);
        _isMoving = false;
    }
    
    private void DamageEnemiesHit()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, HitRadius, enemyLayers);
        foreach (Collider enemy in hitEnemies)
        {
            if (_collidersCollidedWith.Count == 0)
            {
                var enemyCombat = enemy.GetComponent<CharacterCombat>();
                FillCollidersCollidedWith(enemy);
                enemyCombat.TakeDamage(_boomerangSword.BaseDamage);
            }
            else
            {
                foreach (Collider collided in _collidersCollidedWith)
                {
                    if (!enemy.Equals(collided))
                    {
                        var enemyCombat = enemy.GetComponent<CharacterCombat>();
                        FillCollidersCollidedWith(enemy);
                        enemyCombat.TakeDamage(_boomerangSword.BaseDamage);
                    }
                }
            }
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, HitRadius);
    }
    
    private void FillCollidersCollidedWith(Collider enemy)
    {
        _collidersCollidedWith.Add(enemy);
    }
    
    public void ClearCollidersCollidedWith()
    {
        _collidersCollidedWith.Clear();
    }
}

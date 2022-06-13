using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class TornadoScript : MonoBehaviour
{
    [SerializeField] private GameObject rootedDebuff;
    private Tornado _tornado;
    public GameObject[] SpawnedEnemies { get; set; }
    private float Radius { get; set; }
    private float SlerpSpeed { get; set; }

    private void Awake()
    {
        _tornado = GameObject.FindWithTag("Player").GetComponent<Tornado>();
        Radius = 10f;
        SlerpSpeed = 10f;
    }

    private void Update()
    {
        //TODO: Needs to be Set again when another Enemy Spawns game time.
        SpawnedEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        PullEnemies();
    }

    private void PullEnemies()
    {
        foreach (var enemy in SpawnedEnemies)
        {
            if (Vector3.SqrMagnitude (transform.position - enemy.transform.position) < Radius)
            {
                InstantiateRootDebuff(enemy.transform);
                // Set character state to be in combat
                enemy.GetComponent<CharacterCombat>().UpdateCharacterCombatState(CharacterCombatState.InCombat);
                var randomVector = Random.insideUnitSphere * Radius;
                print("randomVector " + randomVector);
                enemy.transform.position =
                    Vector3.MoveTowards(enemy.transform.position, new Vector3(randomVector.x, enemy.transform.position.y, randomVector.z), 6f * Time.deltaTime);
            }
        }
    }

    
    // TODO: TO be changed and made general to all debuff
    private void InstantiateRootDebuff(Transform enemy)
    {
        // bad name
        if (enemy.Find("RootedEffect(Clone)") == null)
        {
            var cloneDebuff = Instantiate(rootedDebuff, enemy);
            var rooted = cloneDebuff.GetComponent<RootedDebuff>();
            rooted.duration = _tornado.Duration;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, Radius);
    }
}

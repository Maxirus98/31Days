using UnityEngine;

public class TornadoScript : MonoBehaviour
{
    public GameObject[] SpawnedEnemies { get; set; }
    private float Radius { get; set; }
    private float SlerpSpeed { get; set; }

    private void Awake()
    {
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
            if (Vector3.Distance (transform.position, enemy.transform.position) < Radius)
            {
                enemy.transform.position =
                    Vector3.MoveTowards(enemy.transform.position, transform.position + Vector3.up, 6 * Time.deltaTime);
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TsunamiScript : MonoBehaviour
{
    private Rigidbody _rigidbody;
    List<EnemyAnimator> enemies = new List<EnemyAnimator>();

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (enemies.Count > 0)
        {
            PushEnemy();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemies.Add(other.gameObject.GetComponent<EnemyAnimator>());
            _rigidbody.velocity = Vector3.zero;
            StartCoroutine(nameof(MakeTumble));
        }
    }

    private void PushEnemy()
    {
        foreach (var enemy in enemies)
        {
            var rb = enemy.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * -1, ForceMode.Impulse);
        }
    }

    private IEnumerator MakeTumble()
    {
        foreach (var enemy in enemies)
        {
            enemy.AnimateEnemy("Fall");
            yield return new WaitForSeconds(0.1f);
            enemy.StopAnimatingEnemy("Fall");
        }
    }
}

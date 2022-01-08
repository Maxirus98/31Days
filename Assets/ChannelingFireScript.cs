using System.Collections.Generic;
using UnityEngine;

public class ChannelingFireScript : MonoBehaviour
{
    public ParticleSystem ps;
    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
    private GameObject[] _enemies;
 
    void Start () {
        ps = GetComponent<ParticleSystem>();
        _enemies = GameObject.FindGameObjectsWithTag("Enemies");
        for (int i=0;i<_enemies.Length;i++)
        {
            ps.trigger.SetCollider(i, _enemies[i].GetComponent<BoxCollider>());
        }
    }
 
    void OnParticleTrigger()
    {
        int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
 
        for (int i = 0; i < numEnter; i++)
        {
            ParticleSystem.Particle p = enter[i];
            //take damage
            enter[i] = p;
        }
 
        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
 
        Debug.Log("Working!");
    }
}

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public float resource;
    public float maxResource;
    public float defense;
    public float AttackSpeed { get; set; }
    public float MovementSpeed { get; set; }
    public float TurnSpeed { get; set; }

    protected Slider _hpSlider;
    protected Slider _resourceSlider;

    public virtual void Start()
    {
        maxHealth = 100;
        maxResource = 100;
        health = maxHealth;
        resource = maxResource;
    }

    protected virtual IEnumerator SetSliders()
    {
        throw new NotImplementedException();
    }
    public virtual void initStats()
    {
        health = 100;
        maxHealth = health;
        resource = 100;
        maxResource = resource;
        defense = 10;
        AttackSpeed = 0.1f;
        MovementSpeed = 6f;
        TurnSpeed = 500f;
    }
    
}

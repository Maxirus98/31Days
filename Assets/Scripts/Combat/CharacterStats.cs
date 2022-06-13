using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour
{
    public readonly float InitMovementSpeed = 6f;

    public float health;
    public float maxHealth;
    public float resource;
    public float maxResource;
    public float defense;
    public float movementSpeed;
    public float attackSpeed;
    public float TurnSpeed { get; set; }

    protected Slider _hpSlider;
    protected Slider _resourceSlider;

    public virtual void Start()
    {
        health = maxHealth;
        resource = maxResource;
        movementSpeed = InitMovementSpeed;
    }

    protected virtual IEnumerator SetSliders()
    {
        throw new NotImplementedException();
    }
}

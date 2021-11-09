using System;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour
{
    public float Health { get; set; }
    public float MaxHealth { get; set; }
    public float Resource { get; set; }
    public float MaxResource { get; set; }
    public float Defense { get; set; }
    public float CurrentDefenseMultiplier { get; set; }
    public float AttackSpeed { get; set; }
    public float MovementSpeed { get; set; }
    public float TurnSpeed { get; set; }

    private Slider _hpSlider;
    private Slider _resourceSlider;
    private void Awake()
    {
        // Going to be in PlayerStats
        _hpSlider = GameObject.Find("PlayerResource").transform.GetChild(0).gameObject.GetComponent<Slider>();
        _resourceSlider = GameObject.Find("PlayerResource").transform.GetChild(1).gameObject.GetComponent<Slider>();

        initStats();
        
        _hpSlider.maxValue = MaxHealth;
        _resourceSlider.maxValue = MaxResource;
    }

    private void Update()
    {
        _hpSlider.value = Health;
        _resourceSlider.value = Resource;
    }

    public virtual void initStats()
    {
        Health = 100;
        MaxHealth = Health;
        Resource = 100;
        MaxResource = Resource;
        Defense = 10;
        CurrentDefenseMultiplier = 0.33f;
        AttackSpeed = 0.1f;
        MovementSpeed = 6f;
        TurnSpeed = 500f;
    }
}

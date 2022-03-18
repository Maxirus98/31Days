using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : CharacterStats
{
    private Transform _hud;
    public  GameObject hp;
    public override void Start()
    {
        base.Start();
        StartCoroutine(nameof(SetSliders));
    }

    protected override IEnumerator SetSliders()
    {
        yield return new WaitForSeconds(1f);
        _hud = GameObject.Find("HUD").transform;

        hp = AbilityUtils.FindDeepChild("HP", _hud).gameObject;
        _hpSlider = hp.GetComponent<Slider>();
        _resourceSlider =  AbilityUtils.FindDeepChild("Mana", _hud).GetComponent<Slider>();

        _hpSlider.maxValue = maxHealth;
        _resourceSlider.maxValue = maxResource;
    }

    private void Update()
    {
        if (_hpSlider && _resourceSlider)
        {
            _hpSlider.value = health;
            _resourceSlider.value = resource;
        }
    }
}

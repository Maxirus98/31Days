using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : CharacterStats
{
    private Transform _resourceTransform;
    public  GameObject hp;
    public override void Start()
    {
        base.Start();
        StartCoroutine(nameof(SetSliders));
    }

    protected override IEnumerator SetSliders()
    {
        yield return new WaitForSeconds(1f);
        _resourceTransform = GameObject.Find("PlayerResource").transform;

        hp = AbilityUtils.FindDeepChild("HP", _resourceTransform).gameObject;
        _hpSlider = hp.GetComponent<Slider>();
        _resourceSlider =  AbilityUtils.FindDeepChild("Mana", _resourceTransform).GetComponent<Slider>();

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

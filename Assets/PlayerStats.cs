using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : CharacterStats
{
    private Transform _playerResource;
    public  GameObject hp;
    public override void Start()
    {
        base.Start();
        StartCoroutine(nameof(SetSliders));
    }

    protected override IEnumerator SetSliders()
    {
        yield return new WaitForSeconds(1f);
        _playerResource = GameObject.Find("/HUD/PlayerResource").transform;

        hp = _playerResource.Find("HP").gameObject;
        _hpSlider = hp.GetComponent<Slider>();
        _resourceSlider =  AbilityUtils.FindDeepChild("Mana", _playerResource).GetComponent<Slider>();

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

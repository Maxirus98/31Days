using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : CharacterStats
{
    public override void Start()
    {
        base.Start();
        StartCoroutine(nameof(SetSliders));
    }

    protected override IEnumerator SetSliders()
    {
        yield return new WaitForSeconds(1);
        _hpSlider = GameObject.Find("PlayerResource").transform.GetChild(0).gameObject.GetComponent<Slider>();
        _resourceSlider = GameObject.Find("PlayerResource").transform.GetChild(1).gameObject.GetComponent<Slider>();

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

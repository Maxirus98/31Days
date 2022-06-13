using System;
using System.Globalization;
using TMPro;
using UnityEngine;

public class CombatUiScript : MonoBehaviour
{
    private TextMeshProUGUI _cloneText;
    
    [SerializeField]private TextMeshProUGUI damageText;

    private float _duration = 2f;
    // Makes the enemy bug
    /*
     * Steps: Make a text appear
     * Make the text move from bot to top
     * Make the text color change according to how high the damage is
     * Make the text fade away  
     */

    private void Update()
    {
        if (_cloneText)
        {
            _duration -= Time.deltaTime;
 
            if(_duration <= 0.0f){
                _duration = 2.0f;
            }

            _cloneText.alpha = _duration;
        }
    }

    public void PrintDamage(float damage)
    {
        _cloneText = Instantiate(damageText, transform);
        _cloneText.text = damage.ToString(CultureInfo.CurrentCulture);
        Destroy(_cloneText, _duration);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Growth : Spells
{
    private Vector3 _initialScale;
    private void Awake()
    {
            Name = "Growth";
            Description = "For a limited time, you grow in size and in health";
            Cooldown = 1f;
            IsAutoTarget = true;
            _initialScale = transform.localScale;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            StartCoroutine(nameof(DoSpell));

        }
    }


    protected override IEnumerator DoSpell()
    {
        transform.localScale *= 1.5f;
        yield return new WaitForSeconds(5f);    
        transform.localScale = _initialScale;
    }


    protected override IEnumerator AnimatePlayer()
    {
        Debug.Log("animate player with spellName" + Name);
        _playerAnimator.AnimateSpell(Name);
        yield return new WaitForSeconds(0.1f);
        _playerAnimator.StopAnimatingSpell(Name);
    }
}

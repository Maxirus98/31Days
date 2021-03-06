using System.Collections;
using UnityEngine;

public class Roll : Spells
{
    private void Awake()
    {
        Name = "Roll";
        Description = "Roll in the moving direction";
        cooldown = 1f;
        IsAutoTarget = true;
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
        StartCoroutine(nameof(AnimatePlayer));
        yield break;
    }

    protected override IEnumerator AnimatePlayer()
    {
        playerAnimator.AnimateSpell(Name);
        yield return new WaitForSeconds(0.1f);
        playerAnimator.StopAnimatingSpell(Name);
    }
}

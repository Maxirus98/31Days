using System.Collections;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class BladeDance : AutoTargetSpell
{
    [SerializeField]private GameObject _dagger;
    private GameObject cloneDaggerRight;
    private GameObject cloneDaggerLeft;
    private void Awake()
    {
        Name = "Blade Dance";
        Description = "2 consecutive attacks. Blade Dance is more effective while stealth.";
        Cooldown = 1f;
        Range = 130f;
        IsAutoTarget = false;
    }

    private void Update()
    {
        if (Time.time >= Timestamp && Input.GetKeyDown(KeyCode.Alpha1) && IsInRange(Range))
        {
            StartCoroutine(nameof(DoSpell));
        }

        //TODO: I don't like the code
        if (cloneDaggerLeft && cloneDaggerRight)
        {
            var focusPosition = MouseManager.focus.transform.position;
            cloneDaggerRight.transform.position = Vector3.MoveTowards(cloneDaggerRight.transform.position, focusPosition, 10f * Time.deltaTime);
            cloneDaggerLeft.transform.position = Vector3.MoveTowards(cloneDaggerLeft.transform.position, focusPosition, 10f * Time.deltaTime);
            if (Vector3.SqrMagnitude(cloneDaggerLeft.transform.position - focusPosition) < MouseManager.focus.transform.localScale.y || Vector3.SqrMagnitude(cloneDaggerRight.transform.position - focusPosition) < MouseManager.focus.transform.localScale.y)
            {
                DestroyDaggers();
            }
        }
    }

    protected override IEnumerator DoSpell()
    {
        if (MouseManager && MouseManager.focus)
        {
            StartCoroutine(nameof(AnimatePlayer));
            SpawnDaggers();
            Timestamp = Time.time + Cooldown;
        }
        
        yield return new WaitForSeconds(1f);    
    }


    protected override IEnumerator AnimatePlayer()
    {
        AutoAttack.Blocked = true;
        _playerAnimator.AnimateSpell(Name);
        yield return new WaitForSeconds(0.1f);
        _playerAnimator.StopAnimatingSpell(Name);
        AutoAttack.Blocked = false;
    }

    private void SpawnDaggers()
    {
        cloneDaggerRight = Instantiate(_dagger, MouseManager.focus.transform.position + Vector3.up * 6 + Vector3.right, transform.rotation);
        cloneDaggerLeft = Instantiate(_dagger, cloneDaggerRight.transform.position + Vector3.left * 2 , transform.rotation);
        
        cloneDaggerRight.transform.Rotate(new Vector3(0, 0, -150));
        cloneDaggerLeft.transform.Rotate(new Vector3(0, 0, 150));
    }

    private void DestroyDaggers()
    {
        Destroy(cloneDaggerLeft);
        Destroy(cloneDaggerRight);
    }
}
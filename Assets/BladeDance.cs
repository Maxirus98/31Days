using System.Collections;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class BladeDance : AutoTargetSpell
{
    public bool isBuffed;

    [SerializeField]private GameObject _dagger;
    private GameObject cloneDaggerRight;
    private GameObject cloneDaggerLeft;
    public float initialDamage;
    private void Awake()
    {
        Name = "Blade Dance";
        Description = "2 consecutive attacks. Blade Dance is more effective while stealth.";
        cooldown = 4f;
        BaseDamage = 500f;
        initialDamage = BaseDamage;
        Range = 130f;
        IsAutoTarget = false;
    }

    private void Update()
    {
        base.Update();
        if (Time.time > Timestamp && Input.GetKeyDown(KeyCode.Alpha1))
        {
            StartCoroutine(nameof(DoSpell));
        }

        if (cloneDaggerLeft && cloneDaggerRight)
        {
            if (MouseManager.focus == null)
            {
                DestroyDaggers();
            }

            var focusPosition = MouseManager.focus.transform.position;
            cloneDaggerRight.transform.position = Vector3.MoveTowards(cloneDaggerRight.transform.position, focusPosition, 15f * Time.deltaTime);
            cloneDaggerLeft.transform.position = Vector3.MoveTowards(cloneDaggerLeft.transform.position, focusPosition, 15f * Time.deltaTime);
            if(Vector3.SqrMagnitude(cloneDaggerLeft.transform.position - focusPosition) < MouseManager.focus.transform.localScale.y)
            {
                DestroyDaggers();
                if(isBuffed)
                    DebuffDamage();
            }
        }
    }

    protected override IEnumerator DoSpell()
    {
        if (MouseManager && MouseManager.focus && IsInRange(Range))
        {
            Timestamp = Time.time + cooldown;
            cloneDmgSender = Instantiate(damageSender, transform.position, Quaternion.identity);
            DamageDid = false;
            SpawnDaggers();
            _playerAnimator.AnimateSpell(Name);
            yield return new WaitForSeconds(0.1f);
            _playerAnimator.StopAnimatingSpell(Name);
            attackCallback(CharacterState.InCombat);
        }
    }

    private void SpawnDaggers()
    {
        cloneDaggerRight = Instantiate(_dagger, MouseManager.focus.transform.position + Vector3.up * 6 + Vector3.right, transform.rotation);
        cloneDaggerLeft = Instantiate(_dagger, cloneDaggerRight.transform.position + Vector3.left * 2 , transform.rotation);
        
        cloneDaggerRight.transform.Rotate(new Vector3(0, 0, -150));
        cloneDaggerLeft.transform.Rotate(new Vector3(0, 0, 150));
    }

    private void DestroyDaggers(float time = 0f)
    {
        Destroy(cloneDaggerLeft, time);
        Destroy(cloneDaggerRight, time);
    }

    public void BuffDamage()
    {
        BaseDamage *= 2;
        isBuffed = true;
    }

    public void DebuffDamage()
    {
        BaseDamage = initialDamage;
        isBuffed = false;
    }
}
using System.Collections;
using UnityEngine;

public class AutoTargetSpell : Spells
{
    protected MouseManager MouseManager;
    protected Interactable tmpFocus;
    
    public GameObject damageSender;
    protected GameObject cloneDmgSender;
    protected bool DamageDid = false;
    
    protected override void Start()
    {
        base.Start();
        StartCoroutine(nameof(GetMouseManager));
    }

    protected virtual void Update()
    {
        if (cloneDmgSender && MouseManager.focus)
        {
            SendDamage();
        }
    }

    private void SendDamage()
    {
        var dmgSenderPos = cloneDmgSender.transform.position;
        var focusTransform = MouseManager.focus.transform;
        cloneDmgSender.transform.position = Vector3.MoveTowards(dmgSenderPos,
            focusTransform.position + Vector3.up * 
            focusTransform.localScale.y, 20f * Time.deltaTime);
        if (!DamageDid)
        {
            DamageEnemiesHit();
        }
    }

    private  IEnumerator GetMouseManager()
    {
        yield return new WaitForSeconds(1);
        MouseManager = GameObject.Find("MouseManager").GetComponent<MouseManager>();
    }

    protected bool IsInRange(float range)
    {
        if (MouseManager)
        {
            tmpFocus = MouseManager.focus;
        }
        if(tmpFocus)
        {
            return Vector3.SqrMagnitude(transform.position - tmpFocus.transform.position) < range;
        }
        
        return false;
    }
    
    public bool IsLookingAt(Transform target)
    {
        float dot = Vector3.Dot(transform.forward, (target.position - transform.position).normalized);
        return dot > 0.7f;
    }
    
    private void DamageEnemiesHit()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(cloneDmgSender.transform.position, hitRadius, enemyLayers);
        foreach (Collider enemy in hitEnemies)
        {
            DamageDid = true;
            if (MouseManager.focus.gameObject == enemy.gameObject)
            {
                enemy.GetComponent<CharacterCombat>().TakeDamage(BaseDamage);
                Destroy(cloneDmgSender, 0.2f);
            }
        }
    }
    
    
    private void OnDrawGizmosSelected()
    {
        if (cloneDmgSender == null)
            return;
        Gizmos.DrawWireSphere(cloneDmgSender.transform.position, hitRadius);
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

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

    protected void SendDamage()
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
    
    protected void DamageEnemiesHit()
    {
        int maxColliders = 1;
        Collider[] hitColliders = new Collider[maxColliders];
        int numColliders = Physics.OverlapSphereNonAlloc(cloneDmgSender.transform.position, hitRadius, hitColliders, enemyLayers);
        for(int i = 0; i < numColliders ; i++)
        {
            if ( hitColliders[i].gameObject == MouseManager.focus.gameObject)
            {
                hitColliders[i].GetComponent<CharacterCombat>().TakeDamage(BaseDamage);
                DamageDid = true;
                Destroy(cloneDmgSender, 0.2f);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (cloneDmgSender == null)
            return;
        Gizmos.DrawWireSphere(cloneDmgSender.transform.position, hitRadius);
    }
}

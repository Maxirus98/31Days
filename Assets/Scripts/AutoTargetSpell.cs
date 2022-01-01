using System;
using System.Collections;
using UnityEngine;

public class AutoTargetSpell : Spells
{
    protected MouseManager MouseManager;

    private void Start()
    {
        _playerAnimator = GetComponent<PlayerAnimator>();
        _animator = GetComponent<Animator>();
        StartCoroutine(nameof(GetMouseManager));
    }

    private  IEnumerator GetMouseManager()
    {
        yield return new WaitForSeconds(1);
        MouseManager = GameObject.Find("MouseManager").GetComponent<MouseManager>();
    }
    
    protected bool IsInRange(float range)
    {
        if (MouseManager && MouseManager.focus)
        {
            return Vector3.SqrMagnitude(transform.position - MouseManager.focus.transform.position) < range;
        }
        
        return false;
    }
}

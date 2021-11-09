using System;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;

    private bool isFocus;
    [SerializeField] private Transform _player;

    private bool hasInteracted;

    private void Awake()
    {
        _player = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        if (isFocus && !hasInteracted)
        {
            float distance = Vector3.Distance(_player.position, transform.position);
            if (distance <= radius)
            {
                hasInteracted = true;
            }
        }
        
    }

    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        _player = playerTransform;
        hasInteracted = false;
    }

    public void OnDefocused()
    {
        isFocus = false;
        _player = null;
        hasInteracted = false;
    }
    private void OnDrawGizmosSelected()
    {
        //callback function in unity
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
    
}
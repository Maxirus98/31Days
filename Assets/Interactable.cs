using System;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private static readonly string TargetIndicator = "Target";
    public float radius = 3f;
    
    private bool isFocus;
    
    [SerializeField]
    private GameObject _targeter;
    
    private bool hasInteracted;
    
    private void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.name.Equals(TargetIndicator))
            {
                _targeter = child.gameObject;
            }
        }
    }

    // Set Target here
    public void OnFocused()
    {
        isFocus = true;
        _targeter.SetActive(true);
    }
    
    // Remove target here
    public void OnDefocused()
    {
        isFocus = false;
        _targeter.SetActive(false);
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
    
}
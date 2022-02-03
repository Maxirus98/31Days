using System;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private static readonly string TargetIndicator = "Target";
    private CharacterCombat _characterCombat;
    private MouseManager _mouseManager;
    public float radius = 3f;
    
    private bool isFocus;
    
    [SerializeField]
    private GameObject _targeter;
    
    private bool hasInteracted;
    
    private void Start()
    {
        _characterCombat = GetComponent<CharacterCombat>();
        _mouseManager = GameObject.Find("MouseManager").GetComponent<MouseManager>();
        
        foreach (Transform child in transform)
        {
            if (child.name.Equals(TargetIndicator))
            {
                _targeter = child.gameObject;
            }
        }
    }

    private void Update()
    {
        if (_characterCombat.isDead && isFocus)
        {
            OnDefocused();
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
        _mouseManager.focus = null;
        _targeter.SetActive(false);
    }
}
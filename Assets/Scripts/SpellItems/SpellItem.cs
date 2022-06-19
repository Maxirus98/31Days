using UnityEngine;

/// <summary>
/// A SpellItem is the foundation that every spell has.
/// </summary>
public class SpellItem : ScriptableObject
{
    public GameObject spellFX;
    public string spellAnimation;
    public new string name;
    public string description;
    public float cooldown;
    public float castTime;
    
    public virtual void OnSpellCast()
    {
        Debug.Log("You cast a spell.");
    }
}

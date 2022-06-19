using UnityEngine;

[CreateAssetMenu(menuName = "Spells/BuffSpell")]
public class BuffSpell : SpellItem
{
    public float duration;
    
    /// <summary>
    /// Spell Cast on animation event
    /// </summary>
    public override void OnSpellCast()
    {
        Debug.Log("You cast a spell.");
    }
}
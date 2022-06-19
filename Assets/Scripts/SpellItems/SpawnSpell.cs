using UnityEngine;

[CreateAssetMenu(menuName = "Spells/SpawnSpell")]
public class SpawnSpell : SpellItem
{
    /// <summary>
    /// Object to activate from the player spell pool. (Object Pooling Pattern).
    /// </summary>
    public string gameObjectName;
    
    /// <summary>
    /// Indicator object to activate from the player indicator spell pool. (Object Pooling Pattern).
    /// </summary>
    public string spellIndicatorName;
    public float range;
}
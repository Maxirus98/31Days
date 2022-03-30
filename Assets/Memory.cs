using UnityEngine;

/// <summary>
/// The difference between a Spell and a Memory is that Memories are inside the Amnesia Store.
/// They only describe what it does.
/// Can be of type MemoryType
/// </summary>
[CreateAssetMenu(fileName = "New Memory", menuName = "Create a Memory")]
public class Memory : ScriptableObject
{
    public enum MemoryType
    {
        EnemyDebuff,
        Buff,
        Spell,
    }
    
    /// <summary>
    /// Sprite show in the MemorySlot of the AmnesiaStore
    /// </summary>
    public Sprite sprite;

    /// <summary>
    /// Title shown in the MemorySlot of the AmnesiaStore
    /// </summary>
    public string title;

    /// <summary>
    /// Description shown in the MemorySlot of the AmnesiaStore
    /// </summary>
    public string description;

    /// <summary>
    /// Index from 0 to 8 because there is 9 memory slots
    /// </summary>
    public int index;

    /// <summary>
    /// Type of Memory applied
    /// Debuff = to enemy
    /// Buff = to player, passive: no actions are needed to take effect
    /// Spell = to player, active: an action is needed to take effect. It's added to the ExtraBar.
    /// </summary>
    public MemoryType type;
}

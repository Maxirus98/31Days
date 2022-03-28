using UnityEngine;

/// <summary>
/// The difference between a Spell and a Memory is that Memories are inside the Amnesia Store.
/// They only describe what it does.
/// Can be of type MemoryType
/// </summary>
public class Memory : MonoBehaviour
{
    public enum MemoryType
    {
        EnemyDebuff,
        Buff,
        Spell,
    }
    
    // TODO: Should I make it public or use getters and setters without using a property. I need it Serialized.
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

    /// <summary>
    /// Check if the player bought that Memory in the AmnesiaStore
    /// </summary>
    public bool isChosen;

    protected virtual void Start()
    {
        if (sprite == null || string.IsNullOrEmpty(title) || string.IsNullOrEmpty(description))
        {
            Debug.LogWarning($"Make sure that every properties of the memory {name} are initialized.");
        }
    }
}

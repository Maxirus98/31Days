using System;
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
    
    /// <summary>
    /// Sprite show in the MemorySlot of the AmnesiaStore
    /// </summary>
    [SerializeField] private Sprite _sprite;
    /// <summary>
    /// Title shown in the MemorySlot of the AmnesiaStore
    /// </summary>
    public string Title { get; set; }
    /// <summary>
    /// Description shown in the MemorySlot of the AmnesiaStore
    /// </summary>
    public string Description { get; set; }
    /// <summary>
    /// Index from 0 to 8 because there is 9 memory slots
    /// </summary>
    public float Index { get; set; }

    /// <summary>
    /// Type of Memory applied
    /// Debuff = to enemy
    /// Buff = to player, passive: no actions are needed to take effect
    /// Spell = to player, active: an action is needed to take effect. It's added to the ExtraBar.
    /// </summary>
    public MemoryType Type { get; set; }

    protected virtual void Start()
    {
        if (_sprite == null || string.IsNullOrEmpty(Title) || string.IsNullOrEmpty(Description))
        {
            Debug.LogWarning($"Make sure that every properties of the memory {name} are initialized.");
        }
    }
}

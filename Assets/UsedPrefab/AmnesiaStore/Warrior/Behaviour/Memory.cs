using System;
using UnityEngine;

/// <summary>
/// The difference between a Spell and a Memory is that Memories are inside the Amnesia Store and they don't necessary have a key.
/// </summary>
public class Memory : MonoBehaviour
{
    [SerializeField] private Sprite _sprite;
    public string Title { get; set; }
    public string Description { get; set; }
    public bool isActive { get; set; } = false;

    protected virtual void Start()
    {
        if (_sprite == null || string.IsNullOrEmpty(Title) || string.IsNullOrEmpty(Description))
        {
            Debug.LogWarning($"Make sure that every properties of the memory {name} are initialized.");
        }
    }

    protected virtual void RememberMemory()
    {
        throw new NotImplementedException();
    }
}

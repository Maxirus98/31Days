using System.Collections.Generic;
using UnityEngine;

public class Memories : MonoBehaviour
{
    private static Memories _instance;
    /// <summary>
    /// List of memories to show in the AmnesiaStore
    /// </summary>
    public Memory[] memories;

    /// <summary>
    /// List of memories applied to the player.
    /// </summary>
    public List<Memory> chosenMemories = new List<Memory>();

    private void Start()
    {
        _instance = this;
    }

    /// <summary>
    /// Return the memory if found by title
    /// </summary>
    /// <param name="title">Memory title ex: Bleed</param>
    /// <returns>Memory</returns>
    public static Memory GetChosenMemoryByTitle(string title)
    {
        return _instance.GetChosenMemory(title);
    }
    
    private Memory GetChosenMemory(string title)
    {
        return chosenMemories.Find(m => m.title.Equals(title));
    }
}

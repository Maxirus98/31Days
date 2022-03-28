using UnityEngine;

public class Memories : MonoBehaviour
{
    /// <summary>
    /// List of memories to show in the AmnesiaStore
    /// </summary>
    public Memory[] memories;
    
    void Start()
    {
        if (memories == null || memories.Length < 9)
        {
            Debug.Log($"Memories array has {memories.Length} memories");
        }
    }
}

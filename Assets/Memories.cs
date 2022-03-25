using System.Collections.Generic;
using UnityEngine;

public class Memories : MonoBehaviour
{
    public Memory[] memories;
    void Start()
    {
        if (memories == null || memories.Length < 9)
        {
            Debug.Log($"Memories array has {memories.Length} memories");
        }
    }
}

using System.Collections.Generic;
using UnityEngine;

public class Memories : MonoBehaviour
{
    public List<Memory> memories;
    void Start()
    {
        if (memories == null || memories.Count < 9)
        {
            Debug.LogWarning("Memories array needs to have atleast 9 memories");
        }
    }
}

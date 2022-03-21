using System.Collections.Generic;
using UnityEngine;

public class Memories : MonoBehaviour
{
    [SerializeField]private List<Behaviour> _memories;
    void Start()
    {
        if (_memories == null || _memories.Count < 9)
        {
            Debug.LogWarning("Memories array needs to have atleast 9 memories");
        }
    }
}

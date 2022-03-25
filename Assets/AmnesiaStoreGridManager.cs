using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AmnesiaStoreGridManager : MonoBehaviour
{
    /// <summary>
    /// All children of Grid. Each child have an image component, a title and a text to fill.
    /// </summary>
    private Transform[] _slots;
    /// <summary>
    /// The list of memories on the current player
    /// </summary>
    private Memory[] _memoryList;

    private void Start()
    {
        _memoryList = GameObject.FindWithTag("Player").GetComponent<Memories>().memories;
        _slots = new Transform[_memoryList.Length];
        FillGrid();
    }

    private void FillGrid()
    {
        for(int i = 0; i < _memoryList.Length; i++)
        {
            var slot = transform.GetChild(i);
            var memory = _memoryList[i];
            slot.Find("SlotTitle").GetComponent<TextMeshProUGUI>().text = memory.title;
            slot.Find("Text").GetComponent<TextMeshProUGUI>().text = memory.description;
            slot.GetComponent<Image>().sprite = memory.sprite; 
            _slots[memory.index] = slot;
        }
    }
}

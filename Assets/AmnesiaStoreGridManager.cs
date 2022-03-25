using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

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
    
    public void OnPointerEnter(int index)
    {
        _slots[index].Find("Text").GetComponent<TextMeshProUGUI>().gameObject.SetActive(true);
    }

    public void OnPointerExit(int index)
    {
        _slots[index].Find("Text").GetComponent<TextMeshProUGUI>().gameObject.SetActive(false);
    }

    private void FillGrid()
    {
        for(int i = 0; i < _memoryList.Length; i++)
        {
            var slot = transform.GetChild(i);
            var memory = _memoryList[i];
            slot.Find("SlotTitle").GetComponent<TextMeshProUGUI>().text = memory.title;
            var description = slot.Find("Text").GetComponent<TextMeshProUGUI>();
            description.text = memory.description;
            description.gameObject.SetActive(false);
            slot.GetComponent<Image>().sprite = memory.sprite;
            _slots[memory.index] = slot;
            
        }
    }
}

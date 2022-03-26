using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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
    private Memory[] _memories;

    /// <summary>
    /// Memories selected before hitting the Apply button.
    /// </summary>
    private readonly List<Memory> _temporarySelectedMemories = new List<Memory>();

    private void Start()
    {
        _memories = GameObject.FindWithTag("Player").GetComponent<Memories>().memories;
        Debug.Log($"The player has {_memories.Length} memories don't worry if some behaviour are not implemented onEvent!");
        _slots = new Transform[_memories.Length];
        
        FillGrid();
    }

    public void onClick(int index)
    {
        var selectedMemory = _memories[index];
        if (!_temporarySelectedMemories.Contains(selectedMemory))
        {
            _temporarySelectedMemories.Add(selectedMemory);
            Debug.Log($"Selected {selectedMemory.title}");
        }
        else
        {
            _temporarySelectedMemories.Remove(selectedMemory);
            Debug.Log($"Unselected {selectedMemory.title}");
        }
    }

    /// <summary>
    /// Action when clicking on the Apply button in the Amnesia Store
    /// This script is on the Grid game object but it's called on the Apply button in the Footer.
    /// </summary>
    public void OnApply()
    {
        foreach (var memory in _temporarySelectedMemories)
        {
            Debug.Log($"Applied {memory.title}");
            memory.isChosen = true;
        }
        _temporarySelectedMemories.Clear();
    }

    /// <summary>
    /// Action when clicking on the Close button in the AmnesiaStore
    /// This script is on the Grid game object but it's called on the Close button in the Footer.
    /// </summary>
    public void OnClose()
    {
        if (_temporarySelectedMemories.Count == 0)
            return;
        Debug.Log($"{_temporarySelectedMemories.Count} memories were not applied.");
        _temporarySelectedMemories.Clear();
    }
    
    /// <summary>
    /// On the Slot button inside the Grid gameobject
    /// Makes the text appear.
    /// </summary>
    /// <param name="index">the index of the child in the grid</param>
    public void OnPointerEnter(int index)
    {
        _slots[index].Find("Text").GetComponent<TextMeshProUGUI>().gameObject.SetActive(true);
    }

    /// <summary>
    /// On the Slot button inside the Grid gameobject
    /// Makes the text disappear.
    /// </summary>
    /// <param name="index">the index of the child in the grid</param>
    public void OnPointerExit(int index)
    {
        _slots[index].Find("Text").GetComponent<TextMeshProUGUI>().gameObject.SetActive(false);
    }

    private void FillGrid()
    {
        for(int i = 0; i < _memories.Length; i++)
        {
            var slot = transform.GetChild(i);
            var memory = _memories[i];
            slot.Find("SlotTitle").GetComponent<TextMeshProUGUI>().text = memory.title;
            var description = slot.Find("Text").GetComponent<TextMeshProUGUI>();
            description.text = memory.description;
            description.gameObject.SetActive(false);
            slot.GetComponent<Image>().sprite = memory.sprite;
            _slots[memory.index] = slot;
            
        }
    }
}

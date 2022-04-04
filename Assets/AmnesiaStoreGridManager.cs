using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class AmnesiaStoreGridManager : MonoBehaviour
{
    #region Variables
    /// <summary>
    /// All children of Grid. Each child have an image component, a title and a text to fill.
    /// </summary>
    private Transform[] _slots;
    
    /// <summary>
    /// The list of memories on the current player
    /// </summary>
    private Memories _memories;

    /// <summary>
    /// Memories selected before hitting the Apply button.
    /// </summary>
    private readonly List<Memory> _temporarySelectedMemories = new List<Memory>();

    private readonly string PLAYER_TAG = "Player";
    private readonly string DISPLAYED_MEMORIES = "ChosenMemories";
    private readonly string PLAYER_CAMERA = "PlayerCamera";
    private readonly string STORE_CAMERA = "/AmnesiaStoreContainer/AmnesiaStoreCamera";

    /// <summary>
    /// Selected memories to display.
    /// </summary>
    private Image[] _chosenMemoriesToDisplay;

    /// <summary>
    /// PlayerCamera
    /// </summary>
    private Camera _playerCamera;
    
    /// <summary>
    /// 
    /// </summary>
    private Camera _storeCamera;
    #endregion
    
    private void Start()
    {
        _memories = GameObject.FindWithTag(PLAYER_TAG).GetComponent<Memories>();
        Debug.Log($"The player has {_memories.memories.Length} memories don't worry if some behaviour are not implemented onEvent!");
        _slots = new Transform[_memories.memories.Length];
        _chosenMemoriesToDisplay = GameObject.Find(DISPLAYED_MEMORIES).GetComponentsInChildren<Image>();
        _storeCamera = GameObject.Find(STORE_CAMERA).GetComponent<Camera>();
        _playerCamera = GameObject.Find(PLAYER_CAMERA).GetComponent<Camera>();
        FillGrid();
    }

    /// <summary>
    /// Select memories on click
    /// </summary>
    /// <param name="index">The index in the Memory tree</param>
    public void onClick(int index)
    {
        // TODO: If same Row, unselect the other
        var selectedMemory = _memories.memories[index];
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
        for (int i = 0; i < _temporarySelectedMemories.Count; i++)
        {
            var memory = _temporarySelectedMemories[i];
            
            // Set the image of the memories
            var image = _chosenMemoriesToDisplay[i];
            image.sprite = memory.sprite;
            image.color = new Color(255f, 255f, 255f, 255f);
            
            // Set the PointerEventEnter text of the memories
            var enter = image.GetComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerEnter;
            entry.callback.AddListener( (_) => { Tooltip.ShowTooltip(memory.title, memory.description); } );
            enter.triggers.Add(entry);
            _memories.chosenMemories.Add(_temporarySelectedMemories[i]);
        }
        
        _temporarySelectedMemories.Clear();
        AbilityUtils.UpdateView(_storeCamera, _playerCamera);
    }

    /// <summary>
    /// Action when clicking on the Close button in the AmnesiaStore
    /// This script is on the Grid game object but it's called on the Close button in the Footer.
    /// </summary>
    public void OnClose()
    {
        Debug.Log($"{_temporarySelectedMemories.Count} memories were not applied.");
        _temporarySelectedMemories.Clear();
        AbilityUtils.UpdateView(_storeCamera, _playerCamera);
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

    // TODO: Refactor Long Method
    private void FillGrid()
    {
        var memories = _memories.memories;
        for(int i = 0; i < memories.Length; i++)
        {
            var slot = transform.GetChild(i);
            var memory = memories[i];
            slot.Find("SlotTitle").GetComponent<TextMeshProUGUI>().text = memory.title;
            var description = slot.Find("Text").GetComponent<TextMeshProUGUI>();
            description.text = memory.description;
            description.gameObject.SetActive(false);
            slot.GetComponent<Image>().sprite = memory.sprite;
            _slots[memory.index] = slot;
            
        }
    }
}

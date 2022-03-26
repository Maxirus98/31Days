using UnityEngine;
using UnityEngine.EventSystems;

public class SelectHandler : MonoBehaviour, ISelectHandler
{
    public Memory SelectedObject { get; set; }

    public void OnSelect(BaseEventData eventData)
    {
        if (SelectedObject)
            return;
        SelectedObject = eventData.selectedObject.GetComponent<Memory>();
    }
}

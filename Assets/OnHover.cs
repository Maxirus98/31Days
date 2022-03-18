using System.Linq;
using TMPro;
using UnityEngine;

public class OnHover : MonoBehaviour
{
    private GameObject _player;
    private Spells[] _spells;
    private GameObject _descriptionPanel;
    private GameObject _description;
    private GameObject _title;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _spells = _player.GetComponents<Spells>();
        InitDescriptionFrame();
    }

    private void InitDescriptionFrame()
    {
        _descriptionPanel = GameObject.Find("HUD/PlayerResource/Description");
        _title = GameObject.Find("HUD/PlayerResource/Description/Title");
        _description = GameObject.Find("HUD/PlayerResource/Description/Content");
        _descriptionPanel.SetActive(false);
    }

    public void OnPointerEnter(int index)
    {
        _descriptionPanel.SetActive(true);

        var hoveredSpell = _spells.First(s => s.spellSlot == index);
        _title.GetComponent<TextMeshProUGUI>().text = hoveredSpell.Name;
        _description.GetComponent<TextMeshProUGUI>().text = hoveredSpell.Description;
    }

    public void OnPointerExit()
    {
        _descriptionPanel.SetActive(false);
    }
}

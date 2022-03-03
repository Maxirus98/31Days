using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OnHover : MonoBehaviour
{
    private GameObject _player;
    private Spells[] _spells;
    private GameObject _spellDescriptionFrame;
    private GameObject _spellDescription;
    private GameObject _spellTitle;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _spells = _player.GetComponents<Spells>();
        InitDescriptionFrame();
    }

    private void InitDescriptionFrame()
    {
        _spellDescriptionFrame = GameObject.Find("/PlayerResource/SpellDescription");
        _spellTitle = GameObject.Find("/PlayerResource/SpellDescription/Title");
        _spellDescription = GameObject.Find("/PlayerResource/SpellDescription/Content");
        _spellDescriptionFrame.SetActive(false);
    }

    public void OnPointerEnter(int index)
    {
        _spellDescriptionFrame.SetActive(true);

        var hoveredSpell = _spells.First(s => s.spellSlot == index);
        _spellTitle.GetComponent<TextMeshProUGUI>().text = hoveredSpell.Name;
        _spellDescription.GetComponent<TextMeshProUGUI>().text = hoveredSpell.Description;
    }

    public void OnPointerExit()
    {
        _spellDescriptionFrame.SetActive(false);
    }
}

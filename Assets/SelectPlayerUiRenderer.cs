using TMPro;
using UnityEngine;

public class SelectPlayerUiRenderer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]private TextMeshProUGUI _playerClassText;
    void Start()
    {
        var player = GameObject.FindWithTag("Player");
        var playerName = player.name;
        var sanitizedName = player.name.Substring(0, playerName.IndexOf('('));
        _playerClassText.text = sanitizedName;
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            if (child.name.Contains(sanitizedName))
            {
                child.gameObject.SetActive(true);
            }
        }
    }
}

using UnityEngine;

public class ShopScript : MonoBehaviour
{
    [SerializeField] private GameObject store;
    
    private GameObject _hud;
    private void Start()
    {
        _hud = GameObject.Find("PlayerResource");
        AddStoreToHud();
    }

    // TODO: Refactor to CHANGE camera on click of the Shop. it will also rendered canvas to the new camera.
    private void AddStoreToHud()
    {
        var clone = Instantiate(store, _hud.transform);
        var rect = clone.GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector2(200, 0.5f);
        clone.SetActive(false);
    }
}

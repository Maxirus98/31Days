using UnityEngine;

/// <summary>
/// 
/// </summary>
public class Debuff : MonoBehaviour
{
    public string text;
    protected string SubText { get; set; }

    public float duration;

    // Start is called before the first frame update
    private void Start()
    {
        if (duration <= 0f)
        {
            Debug.LogWarning($"On {gameObject.transform.root.name} duration needs to be set to a higher number than 0 for {text}.");
        }
    }
}

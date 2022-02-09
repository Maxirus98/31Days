using UnityEngine;

/**
 * Apply a debuff with info and for a certain duration.
 * The object needs to be destroyed after the duration.
 */
public class DebuffScript : MonoBehaviour
{
    public string text;
    protected string SubText { get; set; }

    public float duration;

    // Start is called before the first frame update
    private void Start()
    {
        SubText = $"The {gameObject.transform.root.name} is {text.ToLower()} for {duration}.";
        if (duration <= 0f)
        {
            Debug.LogWarning($"On {gameObject.transform.root.name} duration needs to be set to a higher number than 0 for {text}.");
        }
    }
}

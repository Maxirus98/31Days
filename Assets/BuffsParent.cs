using UnityEngine;

/// <summary>
/// Gets a reference of all buffs of the character and deactivate them. The buffs are set has children of this gameobject
/// </summary>
public class BuffsParent : MonoBehaviour
{
    public static GameObject[] Buffs;
    // Start is called before the first frame update
    private void Awake()
    {
        Buffs = new GameObject[transform.childCount];
        for (int i = 0; i<transform.childCount; i++)
        {
            var buff = transform.GetChild(i).gameObject;
            buff.SetActive(false);
            Buffs[i] = buff;
            print(Buffs[i].name);
        }
    }
}

using UnityEngine;

public class CharacterSelector : MonoBehaviour
{

    [SerializeField]private GameObject[] charactersUi;
    public static GameObject ChosenCharacter { get; set; }
    
    public void ChooseCharacter(int choice)
    {
        RemovePreviouslySelectedCharacter();
        ChosenCharacter = Instantiate(charactersUi[choice].gameObject);
    }

    private void RemovePreviouslySelectedCharacter()
    {
        if(ChosenCharacter)
            Destroy(ChosenCharacter);
    }
}

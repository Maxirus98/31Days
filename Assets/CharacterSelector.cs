using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelector : MonoBehaviour
{

    [SerializeField]private GameObject[] charactersUi;
    private GameObject chosenCharacter;
    
    public void ChooseCharacter(int choice)
    {
        RemovePreviouslySelectedCharacter();
        chosenCharacter = Instantiate(charactersUi[choice].gameObject);
    }

    private void RemovePreviouslySelectedCharacter()
    {
        if(chosenCharacter)
            Destroy(chosenCharacter);
    }
}

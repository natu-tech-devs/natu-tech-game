using UnityEngine;
using UnityEngine.UIElements;

public class ChooseCharacter : MonoBehaviour
{
    public VisualElement root;
    public Button character1 => root.Q<Button>("character-1");
    public Button character2 => root.Q<Button>("character-2");

    public CharacterInformationModal characterInformationModal;

    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        characterInformationModal = new CharacterInformationModal();
        root.Add(characterInformationModal);

        character1.RegisterCallback<ClickEvent>(evt =>
        {
            SetSelectedCharacterModalInformation(1);
            characterInformationModal.Open();
            Debug.Log("Character 1 selected");
        });
        character2.RegisterCallback<ClickEvent>(evt =>
        {
            SetSelectedCharacterModalInformation(2);
            characterInformationModal.Open();
            Debug.Log("Character 2 selected");
        });
    }

    void SetSelectedCharacterModalInformation(int character)
    {
        if (character == 1)
        {
            characterInformationModal.TitleImagePath = "Images/Characters/character-1-name";
            characterInformationModal.Description = "Iaçu é um personagem muito forte e resistente que pode curar seus aliados em batalha e protegê-los de ataques inimigos.";
            characterInformationModal.Strength = 5;
            characterInformationModal.Healing = 2;
            characterInformationModal.Defense = 3;
        }
        else if (character == 2)
        {
            characterInformationModal.TitleImagePath = "Images/Characters/character-2-name";
            characterInformationModal.Description = "Tupã é um personagem muito resistente e pode curar seus aliados em batalha e protegê-los de ataques inimigos com sua defesa.";
            characterInformationModal.Strength = 2;
            characterInformationModal.Healing = 3;
            characterInformationModal.Defense = 5;
        }

    }
    void SelectCharacter(int character)
    {
        PlayerPrefs.SetInt("Character", character);
        PlayerPrefs.Save();
        Debug.Log("Character selected: " + character);
    }
}

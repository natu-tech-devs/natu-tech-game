using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UIElements;



public class CharacterInformationModal : VisualElement
{
    public new class UxmlFactory : UxmlFactory<CharacterInformationModal, UxmlTraits> { }

    private VisualElement titleImagePath => this.Q<VisualElement>("character-title");
    private Label description => this.Q<Label>("character-description");
    private VisualElement statStrengthContainer => this.Q<VisualElement>("stat-strength");
    private VisualElement statHealingContainer => this.Q<VisualElement>("stat-healing");
    private VisualElement statDefenseContainer => this.Q<VisualElement>("stat-defense");

    public int Strength
    {
        get => int.Parse(statStrengthContainer.Q<Label>().text);
        set
        {
            var container = statStrengthContainer.Q<VisualElement>("stat-dots");
            CreateDots(container, value);
        }
    }

    public int Healing
    {
        get => int.Parse(statHealingContainer.Q<Label>().text);
        set
        {
            var container = statHealingContainer.Q<VisualElement>("stat-dots");
            CreateDots(container, value);
        }
    }

    public int Defense
    {
        get => int.Parse(statDefenseContainer.Q<Label>().text);
        set
        {
            var container = statDefenseContainer.Q<VisualElement>("stat-dots");
            CreateDots(container, value);
        }
    }

    public string Description
    {
        get => description.text;
        set
        {
            description.text = value;
        }
    }
    public string TitleImagePath
    {
        set
        {
            titleImagePath.style.backgroundImage = new StyleBackground(Resources.Load<Sprite>(value));
        }
    }

    private void CreateDots(VisualElement container, int dots)
    {
        container.Clear();
        for (int i = 0; i < dots; i++)
        {
            var dot = new VisualElement();
            dot.AddToClassList("stat-dot");
            container.Add(dot);
        }
    }

    public CharacterInformationModal()
    {
        var asset = Resources.Load<VisualTreeAsset>("Components/Modals/CharacterInformationModal/CharacterInformationModal");

        asset.CloneTree(this);

        this.AddToClassList("modal");
        this.CreateControls();
    }

    private void CreateControls()
    {
        var controlsContainer = this.Q<VisualElement>("modal-controls");

        var confirmButton = new ConfirmButton { Label = "Salvar" };
        var cancelButton = new CancelButton { Label = "Voltar" };

        cancelButton.RegisterCallback<ClickEvent>(ev => Close());

        cancelButton.style.marginRight = 16;
        controlsContainer.Add(cancelButton);
        controlsContainer.Add(confirmButton);
    }

    public void Open()
    {
        this.AddToClassList("open");
        Debug.Log("Modal opened");
    }

    public void Close()
    {
        this.RemoveFromClassList("open");
        Debug.Log("Modal closed");
    }

}

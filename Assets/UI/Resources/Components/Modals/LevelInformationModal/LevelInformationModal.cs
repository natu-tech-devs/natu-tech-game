using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelInformationModal : VisualElement
{
    public new class UxmlFactory : UxmlFactory<MenuButton> { };

    private VisualElement modal => this.Q<VisualElement>("modal-container");
    private Label title => this.Q<Label>("modal-title");
    private Label description => this.Q<Label>("modal-description-text");

    public string Title
    {
        get => title.text;
        set => title.text = CapitalizeTitle(value);
    }

    public string Description
    {
        get => description.text;
        set => description.text = value;
    }

    public LevelInformationModal()
    {
        var asset = Resources.Load<VisualTreeAsset>("Components/Modals/LevelInformationModal/LevelInformationModal");
        asset.CloneTree(this);
        this.AddToClassList("modal");

        CreateControls();
    }

    public string CapitalizeTitle(string text)
    {
        var words = text.Trim().Split(' ');
        return words.Aggregate("", (acc, word) =>
        {
            var articles = new string[] { "de", "da", "do", "dos", "das" };
            if ((word.Length <= 2 || articles.Contains(word.ToLower())) && !word.Equals(words.First()))
            {
                return acc + word.ToLower() + " ";
            }

            return acc + word.First().ToString().ToUpper() + word.Substring(1).ToLower() + " ";
        });
    }

    private void CreateControls()
    {
        var controlsContainer = this.Q<VisualElement>("modal-controls");

        var confirmButton = new ConfirmButton { Label = "Jogar" };
        var cancelButton = new CancelButton { Label = "Voltar" };

        cancelButton.RegisterCallback<ClickEvent>(ev => Close());

        cancelButton.style.marginRight = 16;
        controlsContainer.Add(cancelButton);
        controlsContainer.Add(confirmButton);
    }

    public void Open()
    {
        this.AddToClassList("open");
    }

    public void Close()
    {
        this.RemoveFromClassList("open");
    }
}

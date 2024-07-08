using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

public class ButtonContent
{   public string Text { get; set; }
    public string Icon { get; set; }
}


public class HomeScreen : MonoBehaviour
{
    public VisualTreeAsset visualTree;
    public UIDocument document;
    readonly List<ButtonContent> buttonNames = new() { 
        new() { Text = "Jogar" },
        new() { Text = "Opções", Icon = "settings-icon" },
        new() { Text = "Acessibilidade" },
        new() { Text = "Informações" },
        new() { Text = "Marketplace" },
        new() { Text = "Sair", Icon = "close-icon" }
    };

    void Start()
    {
        this.document = GetComponent<UIDocument>();
        this.CreateMenuButtons();
    }

    void CreateMenuButtons()
    {
        VisualElement menuContainer = document.rootVisualElement.Q("menu-container");

        buttonNames.ForEach((button) => {
            MenuButton menuButton = new() { Text = button.Text };

            if(button.Icon != null)
            {
                var icon = Resources.Load<VectorImage>($"Images/Icons/{button.Icon}");
                menuButton.Icon = icon;
            }
            menuButton.AddToClassList("flex-gap");
            menuContainer.Add(menuButton);
        });
    }
}

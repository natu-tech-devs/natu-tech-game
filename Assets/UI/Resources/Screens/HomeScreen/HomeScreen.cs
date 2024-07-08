using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

public class ButtonContent
{   public string Text { get; set; }
    public string Icon { get; set; }
}


public class HomeScreen : MonoBehaviour
{
    private VisualTreeAsset visualTree;
    private UIDocument document;
    readonly private List<ButtonContent> buttonNames = new() { 
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
        this.HideMenu();
        this.StartCoroutine(this.ShowMenuAfterDelay());
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

    void HideMenu()
    {
        document.rootVisualElement.Q("menu-container").AddToClassList("hide");
    }

    void ShowMenu()
    {
        document.rootVisualElement.Q("menu-container").RemoveFromClassList("hide");
    }

    System.Collections.IEnumerator ShowMenuAfterDelay()
    {
        yield return new WaitForSeconds(0.25f);
        this.ShowMenu();
    }
}

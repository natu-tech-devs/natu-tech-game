using UnityEngine;
using UnityEngine.UIElements;

using Button = UnityEngine.UIElements.Button;

public class MenuButton : VisualElement
{
    public new class UxmlFactory : UxmlFactory<MenuButton> {};
    private Button button => this.Q<Button>("menu-button");
    private Label label => this.Q<Label>("menu-button-text");
    private VisualElement icon => this.Q<VisualElement>("menu-button-icon");

    public string Text {
        get => label.text;
        set => label.text = value.ToUpper();
    }

    public VectorImage Icon {
        set => icon.style.backgroundImage = new StyleBackground(value);
    }

    public MenuButton()
    {
        var asset = Resources.Load<VisualTreeAsset>("Components/Buttons/MenuButton/MenuButton");
        asset.CloneTree(this);
    }

    public void OnClick(EventCallback<ClickEvent> callback) 
    {
        button.RegisterCallback(callback);
    }
}

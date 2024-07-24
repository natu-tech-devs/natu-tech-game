using System;
using UnityEngine.UIElements;

public class CancelButton : VisualElement
{
    public new class UxmlFactory : UxmlFactory<CancelButton> { }
    private Button button => this.Q<Button>("cancel-button");
    public string Label
    {
        get => this.button.text;
        set => this.button.text = value;
    }

    public void OnClick(Action action)
    {
        this.button.RegisterCallback<ClickEvent>(ev => action());
    }
}

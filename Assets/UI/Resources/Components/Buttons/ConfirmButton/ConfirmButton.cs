using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UIElements;
using System;
public class ConfirmButton : VisualElement
{
    public new class UxmlFactory : UxmlFactory<ConfirmButton> { }
    private Button button => this.Q<Button>("confirm-button");
    private Label label => this.Q<Label>("confirm-button-label");
    public string Label
    {
        get => this.label.text;
        set => this.label.text = value.ToUpper();
    }

    public ConfirmButton()
    {
        var asset = Resources.Load<VisualTreeAsset>("Components/Buttons/ConfirmButton/ConfirmButton");
        asset.CloneTree(this);

        var icon = new VisualElement();
        icon.AddToClassList("check-icon");
        button.Add(icon);
    }

    public void OnClick(Action action)
    {
        this.button.RegisterCallback<ClickEvent>(ev => action());
    }
}

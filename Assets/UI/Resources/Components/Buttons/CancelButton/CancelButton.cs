using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UIElements;
using System;
public class CancelButton : VisualElement
{
    public new class UxmlFactory : UxmlFactory<CancelButton> { }
    private Button button => this.Q<Button>("cancel-button");
    private Label label => this.Q<Label>("cancel-button-label");
    public string Label
    {
        get => this.label.text;
        set => this.label.text = value.ToUpper();
    }

    public CancelButton()
    {
        var asset = Resources.Load<VisualTreeAsset>("Components/Buttons/CancelButton/CancelButton");
        asset.CloneTree(this);

        var icon = new VisualElement();
        icon.AddToClassList("cancel-button-icon");
        button.Add(icon);
    }

    public void OnClick(Action action)
    {
        this.button.RegisterCallback<ClickEvent>(ev => action());
    }
}


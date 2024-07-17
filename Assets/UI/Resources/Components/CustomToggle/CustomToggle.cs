using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CustomToggle : VisualElement
{
    public new class UxmlFactory : UxmlFactory<CustomToggle> { };
    private Toggle toggle => this.Q<Toggle>("custom-toggle");
    private VisualElement indicator;

    public CustomToggle() { }

    public CustomToggle(string activeImagePath, string inactiveImagePath)
    {
        var asset = Resources.Load<VisualTreeAsset>("Components/CustomToggle/CustomToggle");
        asset.CloneTree(this);

        indicator = new VisualElement();
        indicator.AddToClassList("custom-toggle-indicator");
        toggle.Add(indicator);

        UpdateIndicator(toggle.value, activeImagePath, inactiveImagePath);
        toggle.RegisterValueChangedCallback((evt) =>
        {
            UpdateIndicator(evt.newValue, activeImagePath, inactiveImagePath);
        });
    }

    private void UpdateIndicator(bool isActive, string activeImagePath, string inactiveImagePath)
    {
        if (isActive)
        {
            indicator.style.backgroundImage = new StyleBackground(Resources.Load<Texture2D>($"Images/Icons/{activeImagePath}"));
        }
        else
        {
            indicator.style.backgroundImage = new StyleBackground(Resources.Load<Texture2D>($"Images/Icons/{inactiveImagePath}"));
        }
    }

    public void OnValueChange(System.Action<bool> action)
    {
        toggle.RegisterValueChangedCallback((evt) =>
        {
            action.Invoke(evt.newValue);
        });
    }
}
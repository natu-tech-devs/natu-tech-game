using UnityEngine;
using UnityEngine.UIElements;

public class CustomSlider : VisualElement
{
    public new class UxmlFactory : UxmlFactory<CustomSlider> {};
    private Label label => this.Q<Label>("custom-slider-label");
    private Slider slider => this.Q<Slider>("custom-slider-content");

    public string Label {
        get => label.text;
        set {
            label.text = value;
            slider.label = value;
            label.style.display = DisplayStyle.Flex;
        }
    }

    public CustomSlider()
    {
        var asset = Resources.Load<VisualTreeAsset>("Components/CustomSlider/CustomSlider");
        asset.CloneTree(this);
        label.style.display = DisplayStyle.None;
    }

}

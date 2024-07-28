using UnityEngine;
using UnityEngine.UIElements;

public class CustomSlider : VisualElement
{
    public new class UxmlFactory : UxmlFactory<CustomSlider> { };
    private Slider slider => this.Q<Slider>("custom-slider-content");

    public string Label
    {
        get => slider.label;
        set => slider.label = value;
    }

    public CustomSlider()
    {
        var asset = Resources.Load<VisualTreeAsset>("Components/CustomSlider/CustomSlider");
        asset.CloneTree(this);
    }

    public void OnValueChange(System.Action<float> action)
    {
        slider.RegisterValueChangedCallback(evt => action(evt.newValue));
    }

}

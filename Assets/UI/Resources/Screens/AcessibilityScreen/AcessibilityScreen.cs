using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

public class AcessibilitySlider
{
    public string Label { get; set; }
}

public class AcessibilityToggle
{
    public string ImageName { get; set; }
}

public class AcessibilityScreen : MonoBehaviour
{
    public VisualElement root;
    public VisualElement acessibiltySliders;
    public VisualElement acessibiltyToggles;

    private readonly List<AcessibilitySlider> sliders = new(){
        new AcessibilitySlider { Label = "Velocidade do Texto" },
        new AcessibilitySlider { Label = "Tamanho do Texto" },
        new AcessibilitySlider { Label = "Volume da Música" },
        new AcessibilitySlider { Label = "Volume dos Sons" },
        new AcessibilitySlider { Label = "Volume da Dublagem" }
    };

    private readonly List<AcessibilityToggle> toggles = new(){
        new AcessibilityToggle { ImageName = "mute" },
        new AcessibilityToggle { ImageName = "voice" },
        new AcessibilityToggle { ImageName = "libras" },
        new AcessibilityToggle { ImageName = "vibration" },
        new AcessibilityToggle { ImageName = "confirm" }
    };

    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        acessibiltySliders = root.Q<VisualElement>("acessibility-sliders");
        acessibiltyToggles = root.Q<VisualElement>("acessibility-toggles");
        CreateSliders();
        CreateToggles();
    }

    void CreateSliders()
    {
        foreach (var slider in sliders)
        {
            var sliderContainer = new VisualElement();
            sliderContainer.AddToClassList("acessibility-slider-container");

            var label = new Label(slider.Label);
            label.AddToClassList("acessibility-slider-label");

            var customSlider = new CustomSlider { Label = slider.Label };
            customSlider.OnValueChange(value => Debug.Log($"Slider {slider.Label} value: {value}"));

            sliderContainer.Add(label);
            sliderContainer.Add(customSlider);

            acessibiltySliders.Add(sliderContainer);
        }
    }

    void CreateToggles()
    {
        for (int idx = 0; idx < toggles.Count; idx++)
        {
            var item = toggles[idx];

            var customToggle = new CustomToggle($"{item.ImageName}-active-icon", $"{item.ImageName}-inactive-icon");
            customToggle.OnValueChange(value => Debug.Log($"Toggle {item.ImageName} value: {value}"));

            if (idx != toggles.Count - 1)
            {
                customToggle.AddToClassList("flex-gap");
            }

            acessibiltyToggles.Add(customToggle);
        }
    }
}

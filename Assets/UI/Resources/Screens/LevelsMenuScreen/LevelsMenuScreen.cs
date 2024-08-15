using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelsMenuScreen : MonoBehaviour
{
    // VisualElement root => GetComponent<UIDocument>().rootVisualElement;
    // LevelInformationModal modal;

    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        root.style.width = new StyleLength(new Length(200, LengthUnit.Percent));
        root.style.height = new StyleLength(new Length(200, LengthUnit.Percent));
        root.style.overflow = Overflow.Visible;
    }

    // void Start()
    // {
    //     this.CreateButton();
    //     this.CreateModal();
    // }
    // void CreateButton()
    // {
    //     var container = root.Q<VisualElement>("container");
    //     var button = new Button { text = "Abrir modal" };
    //     button.RegisterCallback<ClickEvent>(ev => modal.Open());
    //     container.Add(button);
    // }

    // void CreateModal()
    // {
    //     var container = root.Q<VisualElement>("container");
    //     modal = new LevelInformationModal
    //     {
    //         Title = "Serra do Vulcão",
    //         Description = "A Serra do Vulcão é uma região montanhosa que abriga um vulcão ativo. A região é conhecida por suas paisagens exuberantes e por ser um local de grande atividade vulcânica.",
    //     };

    //     container.Add(modal);
    // }

}

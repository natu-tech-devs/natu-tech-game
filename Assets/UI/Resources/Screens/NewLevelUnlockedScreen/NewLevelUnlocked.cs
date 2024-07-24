using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NewLevelUnlocked : MonoBehaviour
{
    private UIDocument document;
    void Start()
    {
        document = GetComponent<UIDocument>();

        var container = document.rootVisualElement.Q<VisualElement>("button-container");
        var button = new ConfirmButton { Label = "Jogar" };
        button.OnClick(() =>
        {
            Debug.Log("Button clicked");
        });

        container.Add(button);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

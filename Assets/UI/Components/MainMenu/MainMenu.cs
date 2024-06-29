using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class MainMenu : MonoBehaviour
{
    private UIDocument document;
    private VisualElement root;

    private Button startButton;

    [SerializeField]
    public UnityEvent startEvent { get; private set; }
    void Start()
    {
        document = GetComponent<UIDocument>();
        root = document.rootVisualElement;
        var startTemplate = root.Q("start");

        startButton = root.Q("start").Q<Button>();

        startEvent = new UnityEvent();
        startEvent.AddListener(() =>
        {
            SceneManager.LoadScene("Assets/Scenes/SampleScene.unity");
            Debug.Log("event callback");
        });

        startButton.RegisterCallback<ClickEvent>((evt) =>
        {
            Debug.Log("click callback");
            startEvent.Invoke();
        });
    }
}

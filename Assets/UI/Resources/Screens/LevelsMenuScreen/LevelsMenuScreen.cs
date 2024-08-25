using UnityEngine;
using UnityEngine.UIElements;

public class NewBehaviourScript : MonoBehaviour
{
    VisualElement root;
    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        var levelsResumeModal = new LevelsResumeModal();
        root.Add(levelsResumeModal);

        var starButton = root.Q<Button>("star-button");
        starButton.RegisterCallback<ClickEvent>(ev =>
        {
            levelsResumeModal.Open();
        });
    }

    // Update is called once per frame
    void Update()
    {

    }
}

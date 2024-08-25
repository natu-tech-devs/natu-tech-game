
using UnityEngine;
using UnityEngine.UIElements;

public class LevelsResumeModalManager : MonoBehaviour
{
    private UIDocument document;

    void Start()
    {
        document = GetComponent<UIDocument>();
        var levelsResumeModal = new LevelsResumeModal();
        document.rootVisualElement.Add(levelsResumeModal);
    }

}

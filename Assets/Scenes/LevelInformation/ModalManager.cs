
using UnityEngine;
using UnityEngine.UIElements;

public class ModalManager : MonoBehaviour
{
    public UIDocument document;
    public LevelInformationModal levelInformationModal;

    void Start()
    {
        levelInformationModal = new LevelInformationModal();
        document.rootVisualElement.Add(levelInformationModal);
    }


    public void OpenLevelInformationModal(string title, string description)
    {
        levelInformationModal.Title = title;
        levelInformationModal.Description = description;
        levelInformationModal.Open();
    }

    public void CloseLevelInformationModal()
    {
        levelInformationModal.Close();
    }
}

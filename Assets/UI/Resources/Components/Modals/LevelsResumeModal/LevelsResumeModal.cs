using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelsResumeModal : VisualElement
{
  public new class UxmlFactory : UxmlFactory<LevelsResumeModal> { };

  public LevelsResumeModal()
  {
    var asset = Resources.Load<VisualTreeAsset>("Components/Modals/LevelsResumeModal/LevelsResumeModal");
    asset.CloneTree(this);
    this.AddToClassList("modal");

    CreateButtons();
  }

  void CreateButtons()
  {
    var levelButtonContainer = this.Q<VisualElement>("level-buttons-container");
    foreach (var level in Enumerable.Range(1, 20))
    {
      var levelButton = new LevelButton { LevelNumber = level.ToString(), Stars = Random.Range(0, 3) };

      if (level > 10) levelButton.Lock();

      levelButtonContainer.Add(levelButton);
    }
  }

  public void Open()
  {
    this.AddToClassList("open");
  }

  public void Close()
  {
    this.RemoveFromClassList("open");
  }
}

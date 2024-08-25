using UnityEngine;
using UnityEngine.UIElements;

public class LevelButton : VisualElement
{
  public new class UxmlFactory : UxmlFactory<LevelButton> { };
  private Label levelNumber => this.Q<Label>("level-button-text");
  private VisualElement starsContainer => this.Q<VisualElement>("stars");

  public bool IsLocked { get; set; }
  public string LevelNumber
  {
    get => levelNumber.text;
    set => levelNumber.text = value;
  }

  public int Stars
  {
    get
    {
      var stars = 0;
      foreach (var star in starsContainer.Children())
      {
        if (star.ClassListContains("star-filled")) stars++;
      }
      return stars;
    }
    set
    {
      SetStars(value);
    }
  }

  public LevelButton()
  {
    var asset = Resources.Load<VisualTreeAsset>("Components/Buttons/LevelButton/LevelButton");
    asset.CloneTree(this);
    this.RegisterCallback<ClickEvent>(e => HandleClick());
  }

  private void HandleClick()
  {
    if (IsLocked) return;
    Debug.Log($"Level {LevelNumber} clicked!");
    Debug.Log($"Stars: {Stars}");
  }

  private void SetStars(int stars)
  {
    if (stars > 3) stars = 3;
    for (int i = 0; i < stars; i++)
    {
      var star = starsContainer.ElementAt(i);
      star.AddToClassList("star-filled");
    }
  }

  public void Lock()
  {
    IsLocked = true;
    this.AddToClassList("locked");
  }

  public void Unlock()
  {
    IsLocked = false;
    this.RemoveFromClassList("locked");
  }

}
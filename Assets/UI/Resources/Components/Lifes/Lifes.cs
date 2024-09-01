using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class Lifes : VisualElement
{
  public new class UxmlFactory : UxmlFactory<Lifes> { };
  private VisualElement lifesContainer => this.Q<VisualElement>("lifes-wrapper");

  public int Total
  {
    get => lifesContainer.childCount;
    set => CreateLifes(value);
  }

  public int Quantity
  {
    get => lifesContainer.Children().Count(life => life.ClassListContains("life"));
    set
    {
      FillLifes(value);
    }
  }

  public Lifes()
  {
    var asset = Resources.Load<VisualTreeAsset>("Components/Lifes/Lifes");
    asset.CloneTree(this);
  }


  private void CreateLifes(int total)
  {
    for (int i = 0; i < total; i++)
    {
      var life = new VisualElement();
      life.AddToClassList("life");
      lifesContainer.Add(life);
    }
  }

  private void FillLifes(int value)
  {
    if (value > Total) value = Total;
    var lifes = lifesContainer.Children().ToList();
    for (int i = 0; i < lifes.Count; i++)
    {
      if (i < value)
      {
        lifes[i].AddToClassList("life-filled");
      }
      else
      {
        lifes[i].RemoveFromClassList("life-filled");
      }
    }
  }

}
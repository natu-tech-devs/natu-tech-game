using UnityEngine;
using UnityEngine.UIElements;

public class AttackButton : VisualElement
{
  public new class UxmlFactory : UxmlFactory<AttackButton> { };
  private Button attackButton;
  private VisualElement attackButtonIcon;
  private Label attackButtonLabel;
  public string Icon
  {
    set => attackButtonIcon.style.backgroundImage = Resources.Load<Texture2D>("Images/Icons/" + value);
  }

  public int Quantity
  {
    get => int.Parse(attackButtonLabel.text);
    set => attackButtonLabel.text = value.ToString();
  }

  public AttackButton()
  {
    var asset = Resources.Load<VisualTreeAsset>("Components/AttackButton/AttackButton");
    asset.CloneTree(this);

    attackButton = this.Q<Button>("attack-button");
    attackButtonIcon = this.Q<VisualElement>("attack-button-icon");
    attackButtonLabel = this.Q<Label>("attack-button-label");
  }
}
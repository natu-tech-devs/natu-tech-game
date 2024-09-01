
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class GameScreen : MonoBehaviour
{
    VisualElement root => GetComponent<UIDocument>().rootVisualElement;
    VisualElement container => root.Q<VisualElement>("container");
    VisualElement attackButtonsContainer => root.Q<VisualElement>("attack-buttons-container");
    VisualElement lifesContainer => root.Q<VisualElement>("lifes-container");
    Label timer => root.Q<Label>("timer-number");

    public int timerValue = 90;

    void Start()
    {
        CreateSidebarMenu();
        CreateAttackButtons();
        CreateLifes();

        StartCoroutine(UpdateTimerCoroutine(timerValue));
    }

    void CreateSidebarMenu()
    {
        var sidebarMenu = new SidebarMenu();
        container.Add(sidebarMenu);
    }

    void CreateAttackButtons()
    {
        var waterBallButton = new AttackButton { Icon = "water-ball-icon", Quantity = 5 };
        var fireBallButton = new AttackButton { Icon = "fire-ball-icon", Quantity = 3 };
        var dirtBallButton = new AttackButton { Icon = "dirt-ball-icon", Quantity = 2 };
        var airBallButton = new AttackButton { Icon = "air-ball-icon", Quantity = 1 };

        attackButtonsContainer.Add(waterBallButton);
        attackButtonsContainer.Add(fireBallButton);
        attackButtonsContainer.Add(dirtBallButton);
        attackButtonsContainer.Add(airBallButton);
    }

    void CreateLifes()
    {
        var Lifes = new Lifes { Total = 5, Quantity = 3 };
        lifesContainer.Add(Lifes);
    }

    public IEnumerator UpdateTimerCoroutine(int time)
    {
        while (time > 0)
        {
            UpdateTimer(time);
            yield return new WaitForSeconds(1);
            time--;
        }
    }

    public void UpdateTimer(int time)
    {
        timer.text = time > 9 ? time.ToString() : $"0{time}";
    }
}
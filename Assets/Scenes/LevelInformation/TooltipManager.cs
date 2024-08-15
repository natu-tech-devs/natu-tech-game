using UnityEngine;
using TMPro;

public class TooltipManager : MonoBehaviour
{
    public GameObject tooltip; // Referência ao objeto Tooltip
    public TMP_Text tooltipText; // Referência ao componente TextMeshProUGUI do Tooltip

    void Start()
    {
        HideTooltip();
    }

    public void ShowTooltip(string message, Vector3 position)
    {
        tooltip.SetActive(true);
        tooltipText.text = message;
        tooltip.transform.position = position;
    }

    public void HideTooltip()
    {
        tooltip.SetActive(false);
    }
}
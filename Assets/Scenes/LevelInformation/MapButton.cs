using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MapButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public string stageName; // Nome da fase
    public TooltipManager tooltipManager; // Referência ao TooltipManager
    public float holdTime = 1f; // Tempo necessário para exibir o tooltip
    public Button button; // Referência ao botão

    private bool isPointerDown = false;
    private float timer = 0f;

    void Awake()
    {
        button = GetComponent<Button>();
    }

    void Update()
    {
        if (isPointerDown && button.interactable) // Verifica se o botão está pressionado e habilitado
        {
            timer += Time.deltaTime;
            if (timer >= holdTime)
            {
                ShowTooltip();
                isPointerDown = false; // Impede que o tooltip apareça várias vezes enquanto o botão estiver pressionado
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPointerDown = true;
        timer = 0f; // Reseta o temporizador ao começar a pressionar
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPointerDown = false;
        timer = 0f; // Reseta o temporizador ao soltar o botão
        HideTooltip();
    }

    private void ShowTooltip()
    {
        // Calcula a posição do tooltip (acima do botão)
        Vector3 tooltipPosition = transform.position + new Vector3(0, GetComponent<RectTransform>().sizeDelta.y, 0);
        tooltipManager.ShowTooltip(stageName, tooltipPosition);
    }

    private void HideTooltip()
    {
        tooltipManager.HideTooltip();
    }
}

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MapButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public string levelTitle; // Nome da fase
    public string description;
    public TooltipManager tooltipManager; // Referência ao TooltipManager
    public ModalManager modalManager; // Referência ao ModalManager
    public float holdTime = 1f; // Tempo necessário para exibir o tooltip
    public Button button; // Referência ao botão

    private bool isPointerDown = false;
    private float timer = 0f;
    private bool tooltipShown = false; // Indica se o tooltip foi exibido

    void Awake()
    {
        button = GetComponent<Button>();
    }

    void Update()
    {
        if (isPointerDown && button.interactable) // Verifica se o botão está pressionado e habilitado
        {
            timer += Time.deltaTime;
            if (timer >= holdTime && !tooltipShown)
            {
                ShowTooltip();
                isPointerDown = false; // Impede que o tooltip apareça várias vezes enquanto o botão estiver pressionado
                tooltipShown = true; // Marca que o tooltip foi exibido
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPointerDown = true;
        timer = 0f; // Reseta o temporizador ao começar a pressionar
        tooltipShown = false; // Reseta a marcação do tooltip
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPointerDown = false;
        if (timer < holdTime)
        {
            OpenModal(); // Abre o modal se o tempo pressionado for menor que o tempo necessário para exibir o tooltip
        }
        timer = 0f; // Reseta o temporizador ao soltar o botão
        HideTooltip();
    }

    private void ShowTooltip()
    {
        // Calcula a posição do tooltip (acima do botão)
        Vector3 tooltipPosition = transform.position + new Vector3(0, GetComponent<RectTransform>().sizeDelta.y, 0);
        tooltipManager.ShowTooltip(levelTitle, tooltipPosition);
    }

    private void HideTooltip()
    {
        tooltipManager.HideTooltip();
    }

    private void OpenModal()
    {
        // Lógica para abrir o modal
        if (button.interactable)
        {
            modalManager.OpenLevelInformationModal(levelTitle, description);
        }
    }
}

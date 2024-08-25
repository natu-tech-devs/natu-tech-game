using UnityEngine;
using UnityEngine.UI;

public class DamageText : MonoBehaviour
{
    public Text damageText;

    public void SetText(string text)
    {
        damageText.text = text;
        // Você pode adicionar lógica aqui para animar o texto, como movê-lo para cima e desaparecer.
    }

    private void Start()
    {
        // Uma animação simples para mover o texto para cima e destruí-lo depois de 1 segundo
        Destroy(gameObject, 1f);  // Destroi o objeto após 1 segundo
    }

    private void Update()
    {
        // Exemplo de animação de mover para cima
        transform.position += Vector3.up * Time.deltaTime;
    }
}

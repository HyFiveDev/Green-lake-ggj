using UnityEngine;
using UnityEngine.UI;

public class vida_dano : MonoBehaviour
{
    public int vidaMaxima = 100;
    public int vidaAtual;
    public Slider barraDeVida; // Arraste o Slider do Canvas aqui

    void Start()
    {
        vidaAtual = vidaMaxima;
        if (barraDeVida != null)
        {
            barraDeVida.maxValue = vidaMaxima;
            barraDeVida.value = vidaMaxima;
        }
    }

    public void TomarDano(int dano)
    {
        vidaAtual -= dano;
        if (barraDeVida != null) barraDeVida.value = vidaAtual;

        if (vidaAtual <= 0) Morrer();
    }

    void Morrer()
    {
        if (gameObject.CompareTag("player"))
        {
            // Lógica de Respawn que você já tinha
            transform.position = Vector3.zero; // Exemplo
            vidaAtual = vidaMaxima;
            if (barraDeVida != null) barraDeVida.value = vidaMaxima;
        }
        else
        {
            Destroy(gameObject); // Inimigo morre e some
        }
    }
}

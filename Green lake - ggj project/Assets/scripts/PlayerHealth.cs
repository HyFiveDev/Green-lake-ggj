using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int vida = 20;
    public int danoPorContato = 10;

    [Header("Barra de HP")]
    public SpriteRenderer barraHP; // objeto filho
    public Sprite imagem1; // normal
    public Sprite imagem2; // quando toma dano

    private void Start()
    {
        barraHP.sprite = imagem1; // começa com imagem normal
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Inimigo"))
        {
            TomarDano(danoPorContato);
            Destroy(collision.gameObject);
        }
    }

    void TomarDano(int dano)
    {
        vida -= dano;

        // troca pra imagem de dano
        barraHP.sprite = imagem2;

        Debug.Log("Vida atual: " + vida);

        if (vida <= 0)
        {
            Morrer();
        }
    }

    void Morrer()
    {
        Debug.Log("Player morreu!");
        SceneManager.LoadScene("Game_Over");
    }
}
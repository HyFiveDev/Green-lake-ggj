using UnityEngine;
using UnityEngine.SceneManagement;

public class Agua : MonoBehaviour
{
    public int vida = 20;
    public int danoPorContato = 10;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Agua"))
        {
            TomarDano(danoPorContato);
        }
    }

    void TomarDano(int dano)
    {
        vida -= dano;

     
        Debug.Log("Vida atual: " + vida);

        if (vida <= 0)
        {
            Morrer();
        }
        void Morrer()
        {
            Debug.Log("Player morreu!");
            SceneManager.LoadScene("Game_Over");
        }
    }
    
}

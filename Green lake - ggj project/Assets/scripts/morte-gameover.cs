using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica se o que atingiu o player foi a bala (Tag "Bullet")
        // ou um inimigo (Tag "Enemy")
        if (collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("Inimigo")) ;
        {
            // Se for um tiro, você pode destruir o projétil para ele não atravessar o corpo
            if (collision.gameObject.CompareTag("Bullet"))
            {
                Destroy(collision.gameObject);
            }

            // Chama a tela de Game Over
            SceneManager.LoadScene("Game_Over");
        }
    }
}

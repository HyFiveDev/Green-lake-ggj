using UnityEngine;
using System.Collections;

public class ArmaInimigo : MonoBehaviour
{
    [Header("Alvo")]
    public Transform jogador;

    [Header("Configurações de Tiro")]
    public GameObject prefabProjetil;
    public Transform pontoDeTiro;
    public float velocidadeProjetil = 12f;
    public float intervaloEntreTiros = 1f; // 1 segundo entre balas
    public float tempoDeVidaDaBala = 5f;   // Destrói após 5 segundos

    [Header("Configurações de Recarga")]
    public int maxTiros = 5;               // Atira 5 vezes
    public float tempoRecarga = 3f;        // Espera 3 segundos

    private int tirosAtuais = 0;
    private bool estaRecarregando = false;
    private float cronometroTiro = 0f;

    void Update()
    {
        // Se não encontrar o jogador, não faz nada
        if (jogador == null) return;

        SeguirProtagonista();

        // Só tenta atirar se não estiver no tempo de recarga
        if (!estaRecarregando)
        {
            cronometroTiro += Time.deltaTime;

            if (cronometroTiro >= intervaloEntreTiros)
            {
                Atirar();
                cronometroTiro = 0;
            }
        }
    }

    void SeguirProtagonista()
    {
        // Calcula a direção e o ângulo para o braço girar
        Vector3 direcao = jogador.position - transform.position;
        float angulo = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg;

        // Aplica a rotação no eixo Z
        transform.rotation = Quaternion.Euler(0, 0, angulo);
    }

    void Atirar()
    {
        // 1. Cria a bala
        GameObject projetil = Instantiate(prefabProjetil, pontoDeTiro.position, pontoDeTiro.rotation);

        // 2. Programa a destruição automática dela em 5 segundos
        Destroy(projetil, tempoDeVidaDaBala);

        // 3. Adiciona velocidade (usa o Rigidbody2D da bala)
        Rigidbody2D rb = projetil.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = pontoDeTiro.right * velocidadeProjetil;
        }

        tirosAtuais++;

        // 4. Se chegou a 5 tiros, inicia a recarga
        if (tirosAtuais >= maxTiros)
        {
            StartCoroutine(Recarregar());
        }
    }

    IEnumerator Recarregar()
    {
        estaRecarregando = true;
        Debug.Log("Recarregando arma...");

        yield return new WaitForSeconds(tempoRecarga);

        tirosAtuais = 0;
        estaRecarregando = false;
        Debug.Log("Recarga concluída!");
    }
}
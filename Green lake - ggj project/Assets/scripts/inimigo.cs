using UnityEngine;
using System.Collections;

public class ArmalInimigo : MonoBehaviour
{
    [Header("Alvo")]
    public Transform jogador;

    [Header("Configurações de Tiro")]
    public GameObject prefabProjetil;
    public Transform pontoDeTiro;
    public float velocidadeProjetil = 12f;
    public float intervaloEntreTiros = 1f;

    [Header("Configurações de Recarga")]
    public int maxTiros = 5;
    public float tempoRecarga = 3f;

    private int tirosAtuais = 0;
    private bool estaRecarregando = false;
    private float cronometroTiro = 0f;

    void Update()
    {
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

    void Atirar()
    {
        // 1. Cria a bala
        GameObject projetil = Instantiate(prefabProjetil, pontoDeTiro.position, pontoDeTiro.rotation);

        // 2. Destrói a bala após 1 segundo
        Destroy(projetil, 1f);

        // 3. Aplica velocidade
        Rigidbody2D rb = projetil.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = pontoDeTiro.right * -1 * velocidadeProjetil;
        }

        tirosAtuais++;

        // 4. Controle de recarga
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
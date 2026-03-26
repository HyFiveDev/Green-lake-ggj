using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InimigoHealth : MonoBehaviour
{
    public int vida = 20;
    public int danoPorContato = 10;
    private bool colidindo = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
        {
            colidindo = true;
        }
        else 
        {
            colidindo = false;
        }
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && colidindo == true)
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
            Destroy(gameObject);
        }
    }
    
}
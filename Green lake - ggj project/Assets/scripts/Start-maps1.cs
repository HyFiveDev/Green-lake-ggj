using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToGame : MonoBehaviour
{
    private void OnMouseDown()
    {
        SceneManager.LoadScene("Mapa1");
    }
}
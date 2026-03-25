using UnityEngine;
using UnityEngine.SceneManagement;

public class GoTo : MonoBehaviour
{
    private void OnMouseDown()
    {
        SceneManager.LoadScene("dialogo");
    }
}
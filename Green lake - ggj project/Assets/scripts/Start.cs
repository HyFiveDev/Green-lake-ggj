using UnityEngine;
using UnityEngine.SceneManagement;
public class StartSceneLoaders : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadScene("Start");
    }
}
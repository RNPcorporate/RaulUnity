using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour
{
     public void StartGame()
    {
        // Carga la escena del juego
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame()
    {
        // Sale del juego
        Application.Quit();
    }
}


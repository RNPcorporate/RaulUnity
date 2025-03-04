using UnityEngine;
using UnityEngine.SceneManagement;

public class PausaScript : MonoBehaviour
{
    public GameObject PanelPausa;

    // Variable para controlar el estado del menú de pausa
    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        PanelPausa.SetActive(true);
        Time.timeScale = 0f; // Detiene el tiempo en el juego
        isPaused = true;
    }

    public void Resume()
    {
        PanelPausa.SetActive(false);
        Time.timeScale = 1f; // Reanuda el tiempo normal
        isPaused = false;
    }

    public void BackToMenu()
    {
        // Carga la escena del juego
        SceneManager.LoadSceneAsync(0);
    }
}

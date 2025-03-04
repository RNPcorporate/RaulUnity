using TMPro;
using UnityEngine;
using UnityEngine.UI; // Asegúrate de incluir esto si estás usando Text de UI
// Si estás usando TextMeshPro, usa: using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText; // Cambia `Text` por `TMP_Text` si usas TextMeshPro
    private float startTime;

    void Start()
    {
        // Guardar el momento en que inicia la escena
        startTime = Time.time;
    }

    void Update()
    {
        // Calcular la puntuación
        float score = 200 - (Time.time - startTime);

        // Actualizar el texto de la UI
        scoreText.text = "Puntuación: " + score.ToString("F2") + " pts"; // "F2" para mostrar solo dos decimales
    }
}


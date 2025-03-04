using UnityEngine;

//Camara del juego atada al jugador.
public class CameraScript : MonoBehaviour
{
    //Valor de asociacion con el player
    public Transform player;
    //valor usado para suavizar la transición de movimiento por el player.
    public float smoothSpeed = 1f;
    //Desajuste entre la posición de la cámara y el player.
    public Vector3 offset;
    //factor del 25% para alejar la cámara.
    private float zoomOutFactor = 1.25f;
    //Tamaño original de la cámara.
    private float originalSize;

    //métodos que se inicializan al comienzo de la partida.
     void Start()
    {
        if (GetComponent<Camera>().orthographic)
        {
            originalSize = GetComponent<Camera>().orthographicSize;
        }
    }

    //Métodos que se inicializan a cada fracción de tiempo.
    void LateUpdate()
    {

          // Comprobar si la tecla Ctrl está siendo presionada
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            // Aumentar el orthographicSize en un 25%
            GetComponent<Camera>().orthographicSize = originalSize * zoomOutFactor;
        }
        else
        {
            // Restaurar el tamaño ortográfico original
            GetComponent<Camera>().orthographicSize = originalSize;
        }

        //Ajustes para centrar la cámara constantemente con el player en el centro.
        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        transform.position = new Vector3(transform.position.x, transform.position.y, offset.z);
    }
}

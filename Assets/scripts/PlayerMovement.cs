using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    //Atributos
    //fuerza del salto
    public float jumpForce = 5f;

    //velocidad de movimiento horizaontal
    public float moveSpeed = 5f;

    //Carga el responsable de colisiones
    private Rigidbody2D rb;

    //Registro que visiona si puede saltar
    private bool isGrounded;

    //Rotación del cuadrado
    public float rotationSpeed = 360f;

    //Rango de detección de paredes
    public float jumpProximity = 0.8f;

    //Registra el inicio para hacer respawn
    private Vector3 startPosition;

    //Almacenamiento del sistema de colisiones y de la posición inicial
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }

    //Comprobción de si se le permite saltar, y cuando moverse horizontalmente
    void Update()
    {
        //si se pulsa el botón  w o flecha arriba
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            //Si toca el suelo puede saltar
            if (isGrounded)
            {
                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                isGrounded = false;
            }
        }


        //Si no toca el suelo se puede mover horizontalmente
        if (!isGrounded)
        {
            float move = Input.GetAxis("Horizontal") * moveSpeed;
            rb.linearVelocity = new Vector2(move, rb.linearVelocity.y);

            //Ata el movimiento horizontal a la rotación.
            if (move != 0)
            {
                float rotationDirection = move > 0 ? 1f : -1f;
                transform.Rotate(0, 0, -rotationDirection * rotationSpeed * Time.deltaTime);
            }
        }

        //Para escalar por paredes.
        if(!isGrounded && IsNearWall() && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            rb.linearVelocity = new Vector2(0, 0);
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            float move = Input.GetAxis("Horizontal") * moveSpeed;
            rb.linearVelocity = new Vector2(move, rb.linearVelocity.y);

            if (move != 0)
            {
                float rotationDirection = move > 0 ? 1f : -1f;
                transform.Rotate(0, 0, -rotationDirection * rotationSpeed * Time.deltaTime);
            }
        }
    }

    //Comprobación de proximidad de paredes para escalar, tag Wall.
    bool IsNearWall()
    {
        Debug.Log("Checking walls with jumpProximity: " + jumpProximity);
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, jumpProximity);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Wall"))
            {
                return true;
            }
        }
        return false;
    }

    //respawn si toca cubos rojos, tag Obstacle.
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Jumpable"))
        {
            isGrounded = true;
            transform.rotation = Quaternion.identity;
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
        transform.position = startPosition;
        }

        if (collision.gameObject.CompareTag("Win"))
        {
        SceneManager.LoadScene(0); // Carga la escena 0, asegúrate de que es la correcta en tu configuración de escenas
        }
    }
}

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Input System
    InputSystem_Actions inputSystemActions;
    InputAction move;
    InputAction jump;

    // Componentes
    private Rigidbody2D rb;

    // Configura��es
    public float speed = 5f;
    public float jumpForce = 10f;

    // Controle do ch�o
    private bool isGrounded;

    // Valores do stick
    [Range(-1f, 1f)]
    public float inputHorizontal;
    [Range(-1f, 1f)]
    public float inputVertical;

    void Awake()
    {
        inputSystemActions = new InputSystem_Actions();
        move = inputSystemActions.Player.Move;
        jump = inputSystemActions.Player.Jump;

        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        move.Enable();
        jump.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
        jump.Disable();
    }

    void Update()
    {
        // L� o input do stick
        Vector2 input = move.ReadValue<Vector2>();
        inputHorizontal = input.x;
        inputVertical = input.y;

        
            rb.linearVelocity = new Vector2(inputHorizontal * speed, rb.linearVelocity.y);

            // Pulo
            if (jump.triggered)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                isGrounded = false; // evita pulo duplo
            }
    
    }

    // Detecta contato com o ch�o
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}


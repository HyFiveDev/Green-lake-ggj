using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Input System
    private InputSystem_Actions inputSystemActions;
    private InputAction move;
    private InputAction jump;

    // Componentes
    private Rigidbody2D rb;

    // Configurações
    public float speed = 5f;
    public float jumpForce = 10f;

    // Controle do chão
    private bool isGrounded;

    // Valores do stick
    [Range(-1f, 1f)] public float inputHorizontal;
    [Range(-1f, 1f)] public float inputVertical;

    void Awake()
    {
        inputSystemActions = new InputSystem_Actions();
        move = inputSystemActions.Player.Move;
        jump = inputSystemActions.Player.Jump;

        // CORREÇÃO: precisa especificar o tipo
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
        // CORREÇÃO: precisa especificar o tipo
        Vector2 input = move.ReadValue<Vector2>();

        inputHorizontal = input.x;
        inputVertical = input.y;

        rb.linearVelocity = new Vector2(inputHorizontal * speed, rb.linearVelocity.y);
        if (isGrounded)
        {
            // CORREÇÃO: velocity foi substituído por linearVelocity

            if (jump.triggered)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                isGrounded = false;
            }
        }
    }

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
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private InputSystem_Actions inputSystemActions;
    private InputAction move;
    private InputAction jump;

    private Rigidbody2D rb;
    private Animator anim; // Componente de animação
    private SpriteRenderer sprite; // Para virar o personagem

    public float speed = 5f;
    public float jumpForce = 10f;
    private bool isGrounded;

    [Range(-1f, 1f)] public float inputHorizontal;
    [Range(-1f, 1f)] public float inputVertical;

    void Awake()
    {
        inputSystemActions = new InputSystem_Actions();
        move = inputSystemActions.Player.Move;
        jump = inputSystemActions.Player.Jump;

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>(); // Inicializa o Animator
        sprite = GetComponent<SpriteRenderer>(); // Inicializa o SpriteRenderer
    }

    private void OnEnable() { move.Enable(); jump.Enable(); }
    private void OnDisable() { move.Disable(); jump.Disable(); }

    void Update()
    {
        Vector2 input = move.ReadValue<Vector2>();
        inputHorizontal = input.x;
        inputVertical = input.y;

        // Movimentação
        rb.linearVelocity = new Vector2(inputHorizontal * speed, rb.linearVelocity.y);

        // --- LÓGICA DE ANIMAÇÃO ---
        if (anim != null)
        {
            // Envia o valor absoluto (sempre positivo) para o parâmetro "Speed"
            // Se inputHorizontal for 0, Speed é 0. Se for 1 ou -1, Speed é 1.
            anim.SetFloat("Speed", Mathf.Abs(inputHorizontal));
        }

        // --- LÓGICA DE VIRAR O SPRITE (FLIP) ---
        if (inputHorizontal > 0) sprite.flipX = false;
        else if (inputHorizontal < 0) sprite.flipX = true;

        // Pulo
        if (isGrounded && jump.triggered)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) isGrounded = false;
    }
}
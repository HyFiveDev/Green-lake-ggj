using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private InputSystem_Actions inputSystemActions;
    private InputAction move;
    private InputAction jump;

    private Rigidbody2D rb;
    private Animator anim;
    private bool ataque = false;// Componente de animação
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

        anim.SetBool("Running", inputHorizontal != 0);
        if (Input.GetMouseButtonDown(1))
        {
            ataque = true;
        }
        else
        {
            ataque = false;
        }
        
        anim.SetBool("Attack", ataque);
        
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
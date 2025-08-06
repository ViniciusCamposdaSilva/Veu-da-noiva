using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))] 
// Automaticamente pede um Controlador de Personagem para tudo funcionar.

public class FirstPersonController : MonoBehaviour
{
    // O c�digo abaixo adiciona vari�veis que ser�o usadas pelo c�digo na movimenta��o.
    // Tamb�m possui um t�tulo para o Inspetor...
    // � uma boa pr�tica n�o deixar vari�veis p�blicas, ent�o -
    // - usei SerializeField para tornar ela modific�vel no Inspetor.

    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float gravity = -15f;
    [SerializeField] private float mouseSensitivity = 4f;
    [SerializeField] private float rotationSmoothness = 15f;

    // Abaixo, mesma coisa, mas s�o vari�veis para checar o ch�o.
    // LayerMask me permite usar layers para dizer o que � ch�o e o que n�o �.
    // Com o Vector3 (x, y, z), d� para criar uma esfera de colis�o na Unity... 

    [Header("Ground Check Settings")]
    [SerializeField] private float groundCheckDistance = 0.4f;
    [SerializeField] private Vector3 groundCheckOffset = new Vector3(0, -0.8f, 0);
    [SerializeField] private LayerMask groundLayer;

    // Permite utilizar a c�mera principal...
    
    [Header("References")]
    [SerializeField] private Transform cameraTransform;

    // New Input System:
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction jumpAction;

    private CharacterController controller;
    private Vector2 moveInput;
    private Vector2 lookInput;
    private Vector3 velocity;

    // Vari�veis de rota��o:
    private float verticalRotation;
    private float horizontalRotation;
    private float targetVerticalRotation;
    private float targetHorizontalRotation;

    private bool isGrounded;

    // Cache para evitar aloca��es.
    // Um erro necessitou relacionado a objetos no sistema de isso:

    private Vector3 checkPosition;
    private Collider[] groundCheckResults = new Collider[1]; // Array pr�-alocado (n�o tenho muita no��o disso.).

    // Variavel pro som
    public AudioSource andandoMadeira;
    void Start()
    {
        

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //Procura pelo componente desejado:
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();

        // Configura a��es do Input System:
        moveAction = playerInput.actions["Move"];
        lookAction = playerInput.actions["Look"];
        jumpAction = playerInput.actions["Jump"];

        // Inicializa as rota��es:
        horizontalRotation = transform.eulerAngles.y; // Euler Angles = 360�
        targetHorizontalRotation = horizontalRotation;
        verticalRotation = cameraTransform.localEulerAngles.x;
        targetVerticalRotation = verticalRotation;

        // Corrige valores de rota��o:
        if (verticalRotation > 180) verticalRotation -= 360;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Uma bool só para setar se o player pode ou não se movimentar
    private bool _canMove = true;

    // Um metódo para permite alterar essa boll
    public void SetControl(bool enabled)
    {
        _canMove = enabled;
        if (!enabled && andandoMadeira.isPlaying)
    {
        andandoMadeira.Stop();
    }
    }
    void Update()
    {

        if (!_canMove) return;
        {

            // Verifica��o de ch�o.
            CheckGrounded();

            // Movimento WASD.
            HandleMovement();

            // L�gica de pulo e gravidade.
            HandleJumpAndGravity();
        }
        
         // Rota��o da c�mera.
            HandleCameraRotation();
    }

    private void HandleMovement()
    {
        // L� se o jogador t� usando WASD e usa isso no vetor2:
        moveInput = moveAction.ReadValue<Vector2>();

        // C�digo de movimento 3D! 
        Vector3 move = (transform.right * moveInput.x + transform.forward * moveInput.y) * walkSpeed;
            if (moveInput != Vector2.zero && isGrounded && !andandoMadeira.isPlaying)
            {
            andandoMadeira.Play();
            }
            else if ((moveInput == Vector2.zero || !isGrounded) && andandoMadeira.isPlaying)
{
    andandoMadeira.Stop();
}


        // O Time.deltaTime � bem legal, j� que calcula o tempo entre cada frame
        // para que o movimento n�o seja baseado em FPS...
        controller.Move(move * Time.deltaTime);
        
    }

    private void HandleCameraRotation()
    {
        // L� se o jogador t� movendo a c�mera e retorna um vetor2:
        lookInput = lookAction.ReadValue<Vector2>();

        // Calcula as rota��es.
        // Mathf.Clamp desempenha um papel importante ao limitar a rota��o vertical.
        targetHorizontalRotation += lookInput.x * mouseSensitivity * Time.deltaTime;
        targetVerticalRotation -= lookInput.y * mouseSensitivity * Time.deltaTime;
        targetVerticalRotation = Mathf.Clamp(targetVerticalRotation, -90f, 90f);

        // Interpola suavemente
        horizontalRotation = Mathf.LerpAngle(horizontalRotation, targetHorizontalRotation, rotationSmoothness * Time.deltaTime);
        verticalRotation = Mathf.LerpAngle(verticalRotation, targetVerticalRotation, rotationSmoothness * Time.deltaTime);

        // Aplica as rota��es.
        // Quaternion �, basicamente, a rota��o num espa�o 3D.
        transform.rotation = Quaternion.Euler(0, horizontalRotation, 0);
        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
    }

    private void HandleJumpAndGravity()
    {
        // L�gica de pulo:
        if (jumpAction.triggered && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }

        // Aplica��o de gravidade:
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void CheckGrounded()
    {
        // Calcula a posi��o da verifica��o de ch�o:
        checkPosition = transform.position + groundCheckOffset;
 
        // Verifica��o de ch�o (tamb�m feito evitando bugs ou erros no garbage collector)
        // Tamb�m n�o manjo muito, pesquisei como resolver s�:
        isGrounded = Physics.OverlapSphereNonAlloc(
            checkPosition,
            groundCheckDistance,
            groundCheckResults,
            groundLayer,
            QueryTriggerInteraction.Ignore
        ) > 0;

        // Reseta a velocidade vertical quando no ch�o.
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }
}
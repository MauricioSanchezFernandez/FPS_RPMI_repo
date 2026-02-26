using UnityEngine;
using UnityEngine.InputSystem;

public class FPController : MonoBehaviour
{

    #region General Variables
    [Header("Movement and look")]
    [SerializeField] GameObject CamHolder; //ref al objeto que tiene como hijo la camara del player
    [SerializeField] float speed = 5f;
    [SerializeField] float sprintSpeed = 8f;
    [SerializeField] float crouchSpeed = 3f;
    [SerializeField] float maxForce = 1f; //fuerza maxima de aceleracion
    [SerializeField] float sensitivity = 0.1f; //sensividlidad del imput del look


    [Header("Jump & GroundCheck")]
    [SerializeField] float jumpForce = 5f;
    [SerializeField] bool isGrounded;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundCheckRadius = 0.3f;
    [SerializeField] LayerMask groundLayer; 


    [Header("Player State Bool")]
    [SerializeField] bool isSprinting;
    [SerializeField] bool isCrouching;


    #endregion

    // variables de ref privadas

    Rigidbody rb; //ref rigidbody
    Animator anim;

    //variables inputs
    Vector2 moveInput;
    Vector2 lookInput;
    float lookRotation;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        //Look del cursor del raton
        Cursor.lockState = CursorLockMode.Locked; //mueve cursor al centro
        Cursor.visible = false; // oculta del cursor de la vista

    }

   
    void Update()
    {
        //GroundCheck
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);
        //dibujar un rayo ficticio en escena para determinar la orientacion de la camara
        Debug.DrawRay(CamHolder.transform.position, CamHolder.transform.forward * 100f, Color.red);



    }


    private void FixedUpdate()
    {
        Movement();


    }

    //despues del update 
    private void LateUpdate()
    {
        CameraLook();
    }



    void CameraLook()
    {
        //rotacion horizontal del cuerpo de personaje
        transform.Rotate(Vector3.up * lookInput.x * sensitivity);
        //rotacion vertical la lleva la camara
        lookRotation += (-lookInput.y * sensitivity);
        lookRotation = Mathf.Clamp(lookRotation, -90, 90);
        CamHolder.transform.localEulerAngles = new Vector3(lookRotation, 0f, 0f);
    
    }


    void Movement()
    { 
        
        Vector3 currentVelocity = rb.linearVelocity; //necesitamos calcurlar la velocidad actual en  rb constantemente
        Vector3 targetVelocity = new Vector3(moveInput.x , 0, moveInput.y); //velocidad a alcanzar = direccion que pulsamos
        //ternario
        targetVelocity *= isCrouching ? crouchSpeed : (isSprinting ? sprintSpeed : speed);
        
        //convertir la direccion local en global
        targetVelocity = transform.TransformDirection(targetVelocity);

        //calcular el cambio de velocidad(aceleracion)
        Vector3 velocityChange = (targetVelocity - currentVelocity);
        velocityChange = new Vector3(velocityChange.x, 0f, velocityChange.z);
        velocityChange = Vector3.ClampMagnitude(velocityChange, maxForce);

        //aplicar la fuerza de movimiento/aceleracion
        rb.AddForce(velocityChange, ForceMode.VelocityChange);


    }

    void Jump()
    {
        if (isGrounded) rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }




    ///
    #region Input Methods

    public void OnMove(InputAction.CallbackContext context)
    { 
        moveInput = context.ReadValue<Vector2>();
    }
    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed) Jump();
    }
    public void OnCrouch(InputAction.CallbackContext context)
    { 
        if (context.performed)
        {

            isCrouching = !isCrouching;

         }
    }
    public void OnSprint(InputAction.CallbackContext context)
    {

        if (context.performed && !isCrouching) isSprinting = true;
        if (context.canceled) isSprinting = false; 

    }


    #endregion


}

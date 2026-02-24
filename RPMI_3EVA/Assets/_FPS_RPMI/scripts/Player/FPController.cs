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
        
    }


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
    
    }
    public void OnCrouch(InputAction.CallbackContext context)
    { 
    
    }
    public void OnSprint(InputAction.CallbackContext context)
    { 
    
    }


    #endregion


}
